using System.ComponentModel.DataAnnotations;

namespace Core.Api.Models.Request.V2
{
    public class UpdateProductDescriptionRequest
    {
        [StringLength(500)]
        public string Description { get; set; }
    }
}
