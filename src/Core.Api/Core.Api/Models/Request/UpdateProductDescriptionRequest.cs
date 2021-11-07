using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Models.Request
{
    public class UpdateProductDescriptionRequest
    {
        [FromQuery(Name = "id")]
        public int Id { get; set; }

        [StringLength(500)]
        [FromQuery(Name = "description")]
        public string Description { get; set; }
    }
}
