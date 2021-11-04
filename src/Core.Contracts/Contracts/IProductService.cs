using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Contracts.Models;

namespace Core.Contracts.Contracts
{
    public interface IProductService
    {
        Products GetProducts();

        Task<Product> GetProductByIdAsync(int id);

        Task UpdateProductDescriptionAsync(int id, string description);
    }
}
