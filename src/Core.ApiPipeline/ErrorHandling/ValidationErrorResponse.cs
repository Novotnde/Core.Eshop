using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.ApiPipeline.ErrorHandling
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse(string errorType, List<string> descriptions, string requestId)
        {
            ErrorType = errorType;
            Descriptions = descriptions;
            RequestId = requestId;
        }

        [Required]
        public string RequestId { get; set; }

        [Required]
        public string ErrorType { get; set; }

        [Required]
        public List<string> Descriptions { get; set; }
    }
}
