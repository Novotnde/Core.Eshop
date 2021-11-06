using Core.Utils;
using Database.CatalogDb.Contracts.Dtos;
using Database.CatalogDb.EFCore.Entities;

namespace Database.CatalogDb.EFCore.Profiles
{
    public class ProductsDbProfile : MapperProfile
    {
        public ProductsDbProfile()
        {
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}
