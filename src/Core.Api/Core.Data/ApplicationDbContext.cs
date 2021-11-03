using System;
using System.Collections.Generic;
using System.Text;
using Core.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }
    }
}
