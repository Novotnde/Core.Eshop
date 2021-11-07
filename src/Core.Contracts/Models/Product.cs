﻿using System.ComponentModel.DataAnnotations;

namespace Core.Contracts.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImgUri { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }
    }
}
