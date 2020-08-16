using Cli.DataAccess;
using Cli.DataAccess.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Cli
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