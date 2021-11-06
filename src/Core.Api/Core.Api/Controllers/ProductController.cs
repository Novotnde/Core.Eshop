
using System.Threading;
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
    [Route("api/products")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]

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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return NotFound(new ErrorResponse("","",""));
            }

            var mapperProduct = _mapper.Map<ProductResponse>(product);

            return Ok(mapperProduct);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetProducts(cancellationToken);
            var mapperProducts = _mapper.Map<ProductsResponse>(products);
            return Ok(mapperProducts);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductDescription(int id, string description, CancellationToken cancellationToken)
        {
            var result = await _productService.TryUpdateProductDescriptionAsync(id, description, cancellationToken);
            if (result == false)
            {
                return NotFound(new ErrorResponse("","",""));
            }

            return NoContent();
        }
    }
}
