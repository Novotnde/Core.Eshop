using System;
using Database.CatalogDb.Contracts.Contracts;
using Database.CatalogDb.EFCore.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForDataAccess(
         this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
