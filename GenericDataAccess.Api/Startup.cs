using Api.DataAccess.Mapper;
using Api.DataAccess.Provider;
using Api.DataAccess.Repositories;
using Api.DataAccess.Repositories.Freaky;
using Api.DataAccess.Repositories.Freaky.Strategies;
using Api.Infrastructure.Errorhandling;
using Api.Infrastructure.Extensions;
using Api.Infrastructure.Provider;
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

            // this is for simple test scenario.
            services.AddSingleton<GenericDbContextV1>();
            services.AddSingleton<GenericDbContextV2>();
            services.AddSingleton<GenericDbContextV3>();

            services.AddSingleton<ErrorHandlingMiddleware>();
            services.AddSingleton<IApiVersionProvider, ApiVersionProvider>();
            services.AddSingleton<IAssemblyTypeProvider, AssemblyTypeProvider>();
            services.AddSingleton<IEntityProvider, EntityProvider>();
            services.AddSingleton<IGenericControllerAttributeProvider, GenericControllerAttributeProvider>();
            services.AddSingleton<TypeSpecificRouteProvider>();
            services.AddSingleton<TypeToControllerNameProvider>();
            services.AddSingleton<IGenericTypeProvider, GenericTypeProvider>();
            services.AddSingleton(typeof(IMapper<>), typeof(GenericMapper<>));

            // ToDo: decide which repository you want to use.
            //services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton(typeof(IRepository<>), typeof(StrategyRepository<>));

            services.AddSingleton(typeof(GetStrategy<>));
            services.AddSingleton(typeof(AddStrategy<>));
            services.AddSingleton(typeof(DeleteStrategy<>));
            services.AddSingleton(typeof(UpdateStrategy<>));

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