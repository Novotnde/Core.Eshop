using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Contracts;
using Core.Contracts.Contracts;
using Core.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProductById(int id)
        {
            var products = _productService.GetProductByIdAsync(id);
            return Ok(products);
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            var result = _productService.CreateProductAsync(product);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateProductDescription(int id, string description)
        {
            var result = _productService.UpdateProductDescriptionAsync(id, description);
            return Ok(result);
        }
    }
}
