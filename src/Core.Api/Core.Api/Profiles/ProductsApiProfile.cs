using Core.Api.Models.Request;
using Core.Api.Models.Response;
using Core.ApiPipeline;
using Core.Contracts.Models;
using Core.Utils;

namespace Core.Api.Profiles
{
    public class ProductsApiProfile : MapperProfile
    {
        public ProductsApiProfile()
        {
            CreateValidMap<Products, ProductsResponse>();
            CreateValidMap<Product, ProductResponse>();
            CreateValidMap<UpdateProductDescriptionRequest, Product>();
        }
    }
}
