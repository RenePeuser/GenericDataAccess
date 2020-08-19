using System.Linq;
using Api.Infrastructure.Errorhandling;
using Api.Infrastructure.Provider;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Extensions
{
    internal static class ApiVersioningExtension
    {
        internal static void AddApiVersioningUltimate(this IServiceCollection services)
        {
            // ToDo: Move into own extension
            services.AddApiVersioning(options =>
            {
                var controllerTypes = new GenericControllerProvider().GetAll().ToList();
                foreach (var controllerType in controllerTypes)
                {
                    var apiVersion = controllerType.GetCustomAttribute<ApiVersionAttribute>();
                    if (apiVersion.IsNull())
                    {
                        throw new ProblemDetailsException(500, $"Missing api version attribute on controller: {controllerType.Name}",
                            $"Missing api version attribute on controller: {controllerType.Name}. please set the ApiVersion attribute to the the named controller.");
                    }

                    var allVersions = apiVersion.Versions.Select(v => v);
                    allVersions.ForEach(version => options.Conventions.Controller(controllerType).HasApiVersion(version));
                }
            });
        }
    }
}