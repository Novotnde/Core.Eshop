using System;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.ApiPipeline.ErrorHandling
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// Middleware to handle uncaught exceptions.
        /// </summary>
        public ErrorHandlingMiddleware(
            RequestDelegate next,
            ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.RequestAborted.IsCancellationRequested)
                {
                    // request canceled -> response payload won't ever be received by anything,
                    // so there's no reason to log it as an error and create error response.
                    return;
                }

                _logger.LogError(ex, ex.Message);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;

                var error = new ErrorResponse(ErrorTypes.UnexpectedError, ex.Message, httpContext.TraceIdentifier);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
