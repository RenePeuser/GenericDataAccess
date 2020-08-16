using System.Linq;
using System.Text.Json.Serialization;
using Api.Controllers;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Api.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<GenericDbContext>();
            services.AddSingleton<EntityProvider>();

            services.AddSingleton<ErrorHandlingMiddleware>();

            services.AddMvc(setup => setup.Conventions.Add(new GenericControllerRouteConvention()))
                    .AddNewtonsoftJson()
                    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                    .ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()));

            services.AddApiVersioning(options =>
            {
                var controllerTypes = new GenericControllerProvider().GetAllGenericControllerTypes().ToList();
                foreach (var controllerType in controllerTypes)
                {
                    options.Conventions.Controller(controllerType).HasApiVersion(new ApiVersion(2, 0));
                }
            });

            services.AddSwaggerGen(options =>
            {
                // Dynamically detect declared versions !!
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Generic API - DbContext<T>", Version = "v1" });
                options.SwaggerDoc("v2", new OpenApiInfo { Title = "Generic API - Repository<T>", Version = "v2" });
                options.OperationFilter<RemoveVersionParameterFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "V2");
            });
        }
    }
}