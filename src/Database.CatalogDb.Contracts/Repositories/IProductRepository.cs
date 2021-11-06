using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Database.CatalogDb.Contracts.Dtos;

namespace Database.CatalogDb.Contracts.Contracts
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);

        Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken);

        Task<bool> TryUpdateProductsDescriptionAsync(int id, string description, CancellationToken cancellationToken);
    }
}
