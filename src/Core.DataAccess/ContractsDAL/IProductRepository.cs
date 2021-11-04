using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.ModelDAL;

namespace Core.DataAccess.ContractsDAL
{
    public interface IProductRepository : IDisposable
    {
        ProductsDAL GetProducts();

        Task<ProductDAL> GetProductByIdAsync(int id);

        Task UpdateProductsDescriptionAsync(int id, string description);
    }
}
