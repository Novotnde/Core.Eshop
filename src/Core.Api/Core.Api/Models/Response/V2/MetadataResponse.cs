using System.ComponentModel.DataAnnotations;

namespace Core.Api.Models.Response.V2
{
    public class MetadataResponse
    {
        [Required]
        public int? Offset { get; set; }

        [Required]
        public int? Limit { get; set; }
    }
}
