using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Database.CatalogDb.Contracts.Contracts;
using Database.CatalogDb.Contracts.Dtos;
using Database.CatalogDb.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.CatalogDb.EFCore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        private CatalogDbContext _db;

        public ProductRepository(IMapper mapper, CatalogDbContext db)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(int? limit, int? offset, CancellationToken cancellationToken)
        {
            IQueryable<ProductEntity> productsQueryable = _db.Product
                .AsNoTracking()
                .OrderBy(x => x.Name)
                .ThenBy(y => y.Id);

            if (offset.HasValue && limit.HasValue)
            {
                productsQueryable = productsQueryable
                    .Skip(offset.Value)
                    .Take(limit.Value);
            }

            var result = await productsQueryable
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ProductDto>>(result);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            var result = await _db.Product
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);
            return _mapper.Map<ProductDto?>(result);
        }

        public async Task<bool> TryUpdateProductsDescriptionAsync(int id, string description, CancellationToken cancellationToken)
        {

            var productToUpdate = await _db.Product
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (productToUpdate == null)
            {
                return false;
            }

            productToUpdate.Description = description;
            _db.Update(productToUpdate);
            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
