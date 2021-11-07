using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Contracts.Models;

namespace Core.Api.Models.Response.V2
{
    public class ProductsResponse
    {
        [Required]
        public IEnumerable<Product> Items { get; set; }

        [Required]
        public MetadataResponse Metadata { get; set; }
    }
}
