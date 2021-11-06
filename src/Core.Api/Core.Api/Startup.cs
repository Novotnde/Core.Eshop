using System.Collections.Generic;
using Core.Api.Models.Response;
using Core.Api.Profiles;
using Core.ApiPipeline.ErrorHandling;
using Core.Logic.Profiles;
using Database.CatalogDb.EFCore.Profiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Core.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core.Api", Version = "v1" });
            });

            services.AddAutoMapper(typeof(ProductsLogicProfile));
            services.AddAutoMapper(typeof(ProductsApiProfile));
            services.AddAutoMapper(typeof(ProductsDbProfile));

            services.AddControllers();

            services.AddServicesForCoreLogic();
            services.AddServicesForDatabaseCatalogEFCore();
            services.AddSqlCatalogDb(_configuration);

            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
                setup.ApiVersionReader = new QueryStringApiVersionReader("version");
            });

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core.Api v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseApiVersioning();
        }

        private static IActionResult ReturnValidationErrorResponse(ActionContext actionContext)
        {
            var errorsDescriptions = new List<string>();
            if (!actionContext.ModelState.IsValid)
            {
                foreach (var state in actionContext.ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errorsDescriptions.Add(error.ErrorMessage);
                    }
                }
            }

            return new BadRequestObjectResult(new ValidationErrorResponse("MODEL_VALIDATION_FAILURE", errorsDescriptions));
        }
    }
}
