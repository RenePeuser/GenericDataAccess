using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Api.Errorhandling
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ProblemDetailsException exception)
            {
                context.Response.Clear();
                context.Response.ContentType = MediaTypeNames.Application.Json;

                if (exception.ProblemDetails.Status != null)
                {
                    context.Response.StatusCode = exception.ProblemDetails.Status.Value;
                }

                await context.Response.WriteAsync(JsonSerializer.Serialize(exception.ProblemDetails));
            }
        }
    }
}