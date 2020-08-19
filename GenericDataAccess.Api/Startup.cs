using Api.DataAccess.Provider;
using Api.DataAccess.Repositories;
using Api.Errorhandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<GenericDbContext>();
            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ErrorHandlingMiddleware>();
            services.AddSingleton<IApiVersionProvider, ApiVersionProvider>();
            services.AddSingleton<IAssemblyTypeProvider, AssemblyTypeProvider>();
            services.AddSingleton<IEntityProvider, EntityProvider>();
            services.AddSingleton<IGenericControllerAttributeProvider, GenericControllerAttributeProvider>();
            services.AddSingleton<TypeSpecificRouteProvider>();
            services.AddSingleton<TypeToControllerNameProvider>();
            services.AddSingleton<IGenericTypeProvider, GenericTypeProvider>();

            // new one comes here :-)
            services.AddMvcUltimate();
            services.AddApiVersioningUltimate();
            services.AddSwaggerGenUltimate();
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

            // new one :-)
            app.UseSwaggerUiUltimate();
        }
    }
}