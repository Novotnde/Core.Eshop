using System;
using Core.DataAccess.ContractsDAL;
using Core.DataAccess.ServiceDAL;

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
