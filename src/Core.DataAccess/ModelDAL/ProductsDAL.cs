using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.ModelDAL
{
    public class ProductsDAL
    {
        public IEnumerable<ProductDAL> Items { get; set; }
    }
}
