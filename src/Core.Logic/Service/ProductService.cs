using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Contracts;
using Core.Contracts.Models;
using Database.CatalogDb.Contracts.Contracts;

namespace Core.Logic.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(id, cancellationToken);
            var mappedProduct = _mapper.Map<Product>(product);
            return mappedProduct;
        }

        public async Task<Products> GetProducts(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsAsync(cancellationToken);
            var mappedProducts = _mapper.Map<Products>(products);
            return mappedProducts;
        }

        public async Task<bool> TryUpdateProductDescriptionAsync(int id, string description, CancellationToken cancellationToken)
        {
            return await _productRepository.TryUpdateProductsDescriptionAsync(id, description, cancellationToken);
        }
    }
}
