
using System.Threading.Tasks;
using AutoMapper;
using Core.Api.Models.Response;
using Core.Contracts.Contracts;
using Core.Contracts.Models;
using Microsoft.AspNetCore.Http;
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
        private readonly IMapper _mapper;

        public ProductController(ILogger<ProductController> logger, IProductService productService, IMapper mapper)
        {
            _logger = logger;
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var mapperProduct = _mapper.Map<ProductResponse>(product);
            return Ok(mapperProduct);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status400BadRequest)]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            var mapperProducts = _mapper.Map<ProductsResponse>(products);
            return Ok(mapperProducts);
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status400BadRequest)]
        public IActionResult UpdateProductDescription(int id, string description)
        {
            var result = _productService.UpdateProductDescriptionAsync(id, description);
            return Ok(result);
        }
    }
}
