using Core.Api.Models.Response;
using Core.ApiPipeline;
using Core.Contracts.Models;

namespace Core.Api.Profiles
{
    public class ProductsApiProfile : MapperProfile
    {
        public ProductsApiProfile()
        {
            //FROMTO
            CreateMap<Products, ProductsResponse>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
