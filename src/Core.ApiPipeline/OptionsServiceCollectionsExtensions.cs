using System;
using Core.ApiPipeline.ErrorHandling;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Options
{
    public static class OptionsServiceCollectionsExtensions
    {
        public static IServiceCollection ConfigureApi(this IServiceCollection services)
        {
            services.Configure((Action<ApiBehaviorOptions>)(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => BadRequestReponse.ReturnValidationErrorResponse(actionContext);
            }));

            return services;
        }
    }
}
