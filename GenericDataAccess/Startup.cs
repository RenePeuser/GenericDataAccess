using GenericDataAccess.DataAccess;
using GenericDataAccess.DataAccess.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace GenericDataAccess
{
    internal class Startup
    {
        internal void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<App>();
            services.AddSingleton<MyDbContext>();
            services.AddSingleton<EntityProvider>();
            services.AddSingleton(typeof(Repository<>));
            services.AddSingleton<EntityFactory>();
        }
    }
}