using Database.CatalogDb.Contracts.Dtos;
using Core.Utils;
using Database.CatalogDb.EFCore.Entities;

namespace Database.CatalogDb.EFCore.Profiles
{
    public class ProductsDbProfile : MapperProfile
    {
        public ProductsDbProfile()
        {
            //FROMTO
            CreateMap<ProductEntity, ProductDto>();
        }
    }
}
