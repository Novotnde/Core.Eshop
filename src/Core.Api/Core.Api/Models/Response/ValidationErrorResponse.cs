using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Api.Models.Response
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse(string errorType, List<string> description)
        {
            ErrorType = errorType;
            Description = description;
        }

        [Required]
        public string ErrorType { get; set; }

        [Required]
        public List<string> Description { get; set; }
    }
}
