using Microsoft.Extensions.DependencyInjection;

namespace GenericDataAccess
{
    internal class AppBuilder
    {
        internal App Build()
        {
            var services = new ServiceCollection();
            var startup = new Startup();

            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<App>();
        }
    }
}