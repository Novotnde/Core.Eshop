using System.Collections.Generic;
using System.Data.Entity;
using Database.CatalogDb.EFCore.Entities;

namespace Database.CatalogDb.EFCore.Migrations.DbConfiguration
{
   /*public class CatalogDBInitializer : CreateDatabaseIfNotExists<CatalogDbContext>
   {
        protected override void Seed(CatalogDbContext context)
        {
            IList<ProductEntity> products = new List<ProductEntity>();

            products.Add(new ProductEntity() { Name = "Product1", ImgUri = "image", Description = "First Product", Price = 20 });
            products.Add(new ProductEntity() { Name = "Product2", ImgUri = "image", Description = "Second Product", Price = 25 });
            products.Add(new ProductEntity() { Name = "Product3", ImgUri = "image", Description = "Shird Product", Price = 50 });

            context.Product.AddRange(products);

            base.Seed(context);
        }
    }*/
}
