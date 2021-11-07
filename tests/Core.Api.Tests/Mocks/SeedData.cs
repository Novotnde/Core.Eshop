using Core.Contracts.Models;
using Database.CatalogDb.EFCore;
using Database.CatalogDb.EFCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Tests.Mocks
{
    internal static class SeedData
    {
        public static void InitializeDbForTests(CatalogDbContext db)
        {
            db.Product.AddRange(GetsProductsSeed());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(CatalogDbContext db)
        {
            db.Product.RemoveRange(db.Product);
            InitializeDbForTests(db);
        }

        public static List<ProductEntity> GetsProductsSeed()
        {
            return new List<ProductEntity>()
            {
                new ProductEntity()
                { 
                    Id = 1,
                    Name = "Apple TV", 
                    Description = "TEST RECORD: Apple TV", 
                    Price = 2000,
                    ImgUri = "image"
                },

                new ProductEntity()
                {
                    Id = 2,
                    Name = "Jelly beans",
                    Description = "TEST RECORD: Jelly beans full of different flavors",
                    Price = 20,
                    ImgUri = "image"
                },

                new ProductEntity()
                {
                    Id = 3,
                    Name = "Mug", 
                    Description = "TEST RECORD: Blue mug",
                    Price = 500,
                    ImgUri = "image"
                }
            };
        }

    }
}
