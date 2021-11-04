using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.ModelDAL
{
    public class ProductDAL
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUri { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
