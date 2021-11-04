using System;
using System.Collections.Generic;
using System.Text;
using Core.ApiPipeline;
using Core.Contracts.Models;
using Core.DataAccess.ModelDAL;

namespace Core.Logic.Profiles
{
    public class ProductsLogicProfile : MapperProfile
    {
        public ProductsLogicProfile()
        {
            //FROMTO
            CreateMap<ProductsDAL, Products>();
            CreateMap<ProductDAL, Product>();
            CreateMap<Products, ProductsDAL>();
            CreateMap<Product, ProductDAL>();
        }
    }
}
