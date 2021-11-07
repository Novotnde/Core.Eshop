
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

        /// <summary>
        /// Search for product by id.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>This method should return product based on its id or StatusCodes.Status404NotFound</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(id, cancellationToken);
            if (product == null)
            {
                return NotFound(new ErrorResponse("Get product by id", "there is no product with such id", "Status404NotFound"));
            }

            var mapperProduct = _mapper.Map<ProductResponse>(product);

            return Ok(mapperProduct);
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>This method should return all products or StatusCodes.Status404NotFound</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductsResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productService.GetProducts(cancellationToken);
            var mapperProducts = _mapper.Map<ProductsResponse>(products);
            return Ok(mapperProducts);
        }

        /// <summary>
        /// This method will search for product by id and updates its decription.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="description">description</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>If update was successful method will return NoContent and if unsuccesful result will be NotFound</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProductDescription(int id, string description, CancellationToken cancellationToken)
        {
            var result = await _productService.TryUpdateProductDescriptionAsync(id, description, cancellationToken);
            if (result == false)
            {
                return NotFound(new ErrorResponse("Update product error","update of product desc returned false", "Status404NotFound"));
            }

            return NoContent();
        }
    }
}
