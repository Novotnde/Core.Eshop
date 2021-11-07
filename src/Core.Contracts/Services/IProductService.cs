using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Contracts.Models;

namespace Core.Contracts.Contracts
{
    public interface IProductService
    {
        Task<Products> GetProducts(int? limit, int? offset, CancellationToken cancellationToken);

        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);

        Task<bool> TryUpdateProductDescriptionAsync(int id, string description, CancellationToken cancellationToken);
    }
}
