using Core.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Contracts
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);

        Products GetProductsAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task UpdateProductDescriptionAsync(int id, string description);

    }
}
