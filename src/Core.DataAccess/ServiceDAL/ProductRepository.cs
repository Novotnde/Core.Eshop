using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.ContractsDAL;
using Core.DataAccess.ModelDAL;

namespace Core.DataAccess.ServiceDAL
{
    public class ProductRepository : IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository()
        {
            _db = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public ProductsDAL GetProducts()
        {
            var products = new ProductsDAL();
            var result = _db.Product.ToList();
            products.Items = result;
            return products;
        }

        public async Task<ProductDAL> GetProductByIdAsync(int id)
        {
            var result = await _db.Product.FindAsync(id);
            return result;
        }

        public async Task UpdateProductsDescriptionAsync(int id, string description)
        {
            var productToUpdate = _db.Product.Where(p => p.Id == id).FirstOrDefault();

            if (productToUpdate != null)
            {
                productToUpdate.Description = description;
                _db.Product.Update(productToUpdate);
                await _db.SaveChangesAsync();
            }
        }
    }
}
