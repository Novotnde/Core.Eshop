using System.ComponentModel.DataAnnotations;

namespace Core.Api.Models.Response
{
    public class ErrorResponse
    {
        public ErrorResponse(string errorType, string description, string requestId)
        {
            ErrorType = errorType;
            Description = description;
            RequestId = requestId;
        }

        [Required]
        public string RequestId { get; set; }

        [Required]
        public string ErrorType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
