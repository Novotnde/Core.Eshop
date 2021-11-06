using System;
using Database.CatalogDb.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.CatalogDb.EFCore
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.Entity<ProductEntity>().ToTable(nameof(Product));
        }
    }
}
