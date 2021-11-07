using System;
using Core.Api.Tests.Mocks;
using Database.CatalogDb.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Core.Api.Tests
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public bool UsedInMemory = false;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
           builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<CatalogDbContext>>()
                    .AddDbContextPool<CatalogDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });
                
                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CatalogDbContext>();
               
                db.Database.EnsureCreated();

                SeedData.InitializeDbForTests(db);
                UsedInMemory = true;
            });
        }
    }
}
