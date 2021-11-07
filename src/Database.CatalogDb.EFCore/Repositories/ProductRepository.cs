using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Database.CatalogDb.Contracts.Contracts;
using Database.CatalogDb.Contracts.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Database.CatalogDb.EFCore.Service
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

        public async Task<IReadOnlyList<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
        {
            var result = await _db.Product
                .AsNoTracking()
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
