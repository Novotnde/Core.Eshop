using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api.Models.Request.V1
{
    public class UpdateProductDescriptionRequest
    {
        [StringLength(500)]
        public string Description { get; set; }
    }
}
