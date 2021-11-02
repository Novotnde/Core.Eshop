using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.Models
{
    public class Product
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
