using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Core.ApiPipeline.ErrorHandling
{
    public static class BadRequestReponse
    {
        public static IActionResult ReturnValidationErrorResponse(ActionContext actionContext)
        {
            var errorsDescriptions = new List<string>();
            if (!actionContext.ModelState.IsValid)
            {
                errorsDescriptions = actionContext.ModelState
                    .SelectMany(x => x.Value.Errors
                        .Select(y => y.ErrorMessage))
                    .ToList();
            }

            return new BadRequestObjectResult(new ValidationErrorResponse(ErrorTypes.ModelValidationFailure, errorsDescriptions, actionContext.HttpContext.TraceIdentifier));
        }
    }
}
