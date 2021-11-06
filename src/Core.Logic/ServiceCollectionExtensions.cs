using System;
using Core.Contracts.Contracts;
using Core.Logic.Service;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesForCoreLogic(
         this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddTransient<IProductService, ProductService>();

            return services;
        }
    }
}
