using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Models.Request.V2
{
    public class ProductsRequest
    {
        [NotNull]
        [Required]
        [FromQuery(Name = "offset")]
        public int? Offset { get; set; } = null!;

        [NotNull]
        [Required]
        [FromQuery(Name = "limit")]
        [Range(1, 10)]
        public int? Limit { get; set; } = null!;
    }
}
