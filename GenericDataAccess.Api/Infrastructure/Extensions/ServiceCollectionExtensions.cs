using System.Linq;
using System.Reflection;
using Api.Infrastructure.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly IAssemblyTypeProvider AssemblyTypeProvider = new AssemblyTypeProvider();

        public static void RegisterAllTypes<TType>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Singleton) where TType : class
        {
        }
    }
}