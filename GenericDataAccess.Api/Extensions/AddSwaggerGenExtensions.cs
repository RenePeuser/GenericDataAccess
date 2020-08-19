using Api.DataAccess.Provider;
using Api.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api
{
    internal static class AddSwaggerGenExtensions
    {
        internal static void AddSwaggerGenUltimate(this IServiceCollection services)
        {
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
    }
}