using Core.Api.Models.Request.V2;
using Core.Api.Models.Response.V2;
using Core.Contracts.Models;
using Core.Utils;

namespace Core.Api.Profiles.V2
{
    public class ProductsApiProfile : MapperProfile
    {
        public ProductsApiProfile()
        {
            CreateValidMap<Products, ProductsResponse>()
                .ForMember(p => p.Metadata, s => s.MapFrom(x => x));
            CreateValidMap<Product, ProductResponse>();
            CreateValidMap<Products, MetadataResponse>();
            CreateValidMap<UpdateProductDescriptionRequest, Product>();
        }
    }
}
