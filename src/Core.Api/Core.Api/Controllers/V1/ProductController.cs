
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Api.Models.Request.V1;
using Core.Api.Models.Response.V1;
using Core.ApiPipeline.ErrorHandling;
using Core.Contracts.Contracts;
using Core.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.Api.Controllers
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route( "api/v{version:apiVersion}/products" )]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]

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
                return NotFound(new ErrorResponse(
                    ErrorTypes.ProductNotFound,
                    ErrorDescription.ProductNotFound,
                    HttpContext.TraceIdentifier));
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
            var products = await _productService.GetProducts(null, null, cancellationToken);
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
        public async Task<IActionResult> UpdateProductDescription(
            int id,
            [FromBody]UpdateProductDescriptionRequest productDescriptionRequest,
            CancellationToken cancellationToken)
        {
            if (productDescriptionRequest == null)
            {
                throw new ArgumentNullException(nameof(productDescriptionRequest));
            }

            var result = await _productService.TryUpdateProductDescriptionAsync(id, productDescriptionRequest.Description, cancellationToken);
            if (result == false)
            {
                return NotFound(new ErrorResponse(
                    ErrorTypes.ProductNotFound,
                    ErrorDescription.ProductNotFound,
                    HttpContext.TraceIdentifier));
            }

            return NoContent();
        }
    }
}
