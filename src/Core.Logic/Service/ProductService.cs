using Core.Contracts.Contracts;
using Core.Contracts.Models;
using Core.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Logic.Service
{
    public class ProductService : IProductService
    {
        ApplicationDbContext _db;

        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task CreateProductAsync(Product product)
        {
            var newProduct = new Product()
            {
                Name = product.Name,
                ImgUri = product.ImgUri,
                Description = product.Description,
                Price = product.Price
            };

            await _db.Product.AddAsync(newProduct);
            await _db.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var result = await _db.Product.FindAsync(id);
            return result;
        }

        public Products GetProductsAsync()
        {
            var products = new Products();
            var result = _db.Product.ToList();
            products.Items = result;
            return products;
        }

        public async Task UpdateProductDescriptionAsync(int id, string description)
        {

            var productToUpdate = _db.Product.Where(p => p.Id == id).FirstOrDefault();

            if(productToUpdate.Description == null)
            {
                productToUpdate.Description = description;
                await _db.Product.AddAsync(productToUpdate);
                await _db.SaveChangesAsync();
            }
            else
            {
                productToUpdate.Description = description;
                _db.Product.Update(productToUpdate);
                await _db.SaveChangesAsync();
            }
        }
    }
}
