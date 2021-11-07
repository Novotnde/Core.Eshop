using System.ComponentModel.DataAnnotations;

namespace Core.Api.Models.Response.V2
{
    public class ProductResponse
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImgUri { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
