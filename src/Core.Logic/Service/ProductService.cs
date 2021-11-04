using System.Threading.Tasks;
using AutoMapper;
using Core.Contracts.Contracts;
using Core.Contracts.Models;
using Core.DataAccess.ContractsDAL;
using Core.DataAccess.ModelDAL;

namespace Core.Logic.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,IMapper mapper )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            var mappedProduct = _mapper.Map<Product>(product);
            return mappedProduct;
        }

        public Products GetProducts()
        {
            var products = _productRepository.GetProducts();
            var mappedProducts = _mapper.Map<Products>(products);
            return mappedProducts;
        }

        public async Task UpdateProductDescriptionAsync(int id, string description)
        {
            await _productRepository.UpdateProductsDescriptionAsync(id, description);
        }
    }
}
