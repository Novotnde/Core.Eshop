using System.Collections.Generic;
using Core.Contracts.Models;
using Core.Utils;
using Database.CatalogDb.Contracts.Dtos;

namespace Core.Logic.Profiles
{
    public class ProductsLogicProfile : MapperProfile
    {
        public ProductsLogicProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<IEnumerable<ProductDto>, Products>()
                .ForMember(p => p.Items, s => s.MapFrom(x => x));
        }
    }
}
