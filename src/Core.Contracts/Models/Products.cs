using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Models
{
    public class Products
    {
        public IEnumerable<Product> Items { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
