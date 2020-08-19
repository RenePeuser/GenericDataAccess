using System.Linq;
using System.Text.Json.Serialization;
using Api.Controllers;
using Api.Controllers.Attributes;
using Api.DataAccess;
using Api.DataAccess.Provider;
using Api.Errorhandling;
using Api.Swagger;
using Extensions.Pack;
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
            services.AddSingleton(typeof(Repository<>));
            services.AddSingleton<ErrorHandlingMiddleware>();
            services.AddSingleton<IApiVersionProvider, ApiVersionProvider>();
            services.AddSingleton<IAssemblyTypeProvider, AssemblyTypeProvider>();
            services.AddSingleton<IEntityProvider, EntityProvider>();
            services.AddSingleton<IGenericControllerAttributeProvider, GenericControllerAttributeProvider>();
            services.AddSingleton<TypeSpecificRouteProvider>();
            services.AddSingleton<TypeToControllerNameProvider>();
            services.AddSingleton<IGenericTypeProvider, GenericTypeProvider>();

            // ToDo: Move into own extension
            services.AddMvc(setup => setup.Conventions.Add(new GenericControllerRouteConvention()))
                    .AddNewtonsoftJson()
                    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                    .ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()));

            // ToDo: Move into own extension
            services.AddApiVersioning(options =>
            {
                var controllerTypes = new GenericControllerProvider().GetAll().ToList();
                foreach (var controllerType in controllerTypes)
                {
                    var apiVersion = controllerType.GetCustomAttribute<ApiVersionAttribute>();
                    if (apiVersion.IsNull())
                    {
                        throw new ProblemDetailsException(500, $"Missing api version attribute on controller: {controllerType.Name}", $"Missing api version attribute on controller: {controllerType.Name}. please set the ApiVersion attribute to the the named controller.");
                    }
                    var allVersions = apiVersion.Versions.Select(v => v);
                    allVersions.ForEach(version => options.Conventions.Controller(controllerType).HasApiVersion(version));
                }
            });


            // ToDo: Move into own extension
            services.AddSwaggerGen(options =>
            {
                foreach (var apiVersion in new ApiVersionProvider().GetAll())
                {
                    // ToDo: would be also nice add info to the api description attribute for even more fun :-)
                    options.SwaggerDoc($"v{apiVersion.MajorVersion}.{apiVersion.MinorVersion}", new OpenApiInfo { Title = "Generic API", Version = $"v{apiVersion.MajorVersion}" });
                }

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
                foreach (var apiVersion in new ApiVersionProvider().GetAll())
                {
                    c.SwaggerEndpoint($"/swagger/v{apiVersion.MajorVersion}.{apiVersion.MinorVersion}/swagger.json", $"V{apiVersion.MajorVersion}.{apiVersion.MinorVersion}");
                }
            });
        }
    }
}