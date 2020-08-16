using System.Linq;
using Api.Errorhandling;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Swagger
{
    public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = GetOpenApiPath(swaggerDoc, context);
        }

        private OpenApiPaths GetOpenApiPath(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                var matchingApi =
                    context.ApiDescriptions.FirstOrDefault(api => api.RelativePath.EqualsTo(path.Key.TrimStart('/')));
                var versionInfo = matchingApi.ActionDescriptor.EndpointMetadata.FirstOfType<ApiVersionAttribute>();
                if (versionInfo.IsNull())
                {
                    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
                    continue;
                }

                var apiVersion = Convert(swaggerDoc.Info);
                if (versionInfo.Versions.Any(v => v.EqualsTo(apiVersion)))
                {
                    paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
                }
            }

            return paths;
        }

        private ApiVersion Convert(OpenApiInfo openApiInfo)
        {
            var values = openApiInfo.Version.ToLower().Replace("v", string.Empty).Split(".")
                .Select(number => number.ToInt()).ToArray();
            if (values.Length == 1)
            {
                return new ApiVersion(values.First(), 0);
            }

            if (values.Length == 2)
            {
                return new ApiVersion(values.First(), values.ElementAt(1));
            }

            throw new ProblemDetailsException(500, "Unknown version string", $"Could not convert swagger version info: '{openApiInfo.Version}' into AP-Version");
        }
    }
}