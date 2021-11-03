using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Models
{
    public class Products
    {
        public IEnumerable<Product> Items  { get; set; }
    }
}
