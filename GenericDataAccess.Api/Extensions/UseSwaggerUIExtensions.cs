using Api.DataAccess.Provider;
using Microsoft.AspNetCore.Builder;

namespace Api
{
    internal static class UseSwaggerUIExtensions
    {
        internal static void UseSwaggerUiUltimate(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                foreach (var apiVersion in new ApiVersionProvider().GetAll())
                {
                    c.SwaggerEndpoint($"/swagger/v{apiVersion.MajorVersion}.{apiVersion.MinorVersion}/swagger.json", $"V{apiVersion.MajorVersion}.{apiVersion.MinorVersion}");
                }
            });
        }
    }
}