using System;
using System.Data.Common;
using Database.CatalogDb.Contracts.Contracts;
using Database.CatalogDb.EFCore;
using Database.CatalogDb.EFCore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlCatalogDb(
         this IServiceCollection services, IConfiguration configuration )
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("CatalogDb")));

            //Todo samostatna metoda
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
