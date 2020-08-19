using System.Text.Json.Serialization;
using Api.Infrastructure.Attributes;
using Api.Infrastructure.Provider;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Extensions
{
    public static class MvcExtensions
    {
        public static void AddMvcUltimate(this IServiceCollection services)
        {
            services.AddMvc(setup => setup.Conventions.Add(new GenericControllerRouteConvention()))
                .AddNewtonsoftJson()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .ConfigureApplicationPartManager(p => p.FeatureProviders.Add(new GenericControllerFeatureProvider()));
        }
    }
}