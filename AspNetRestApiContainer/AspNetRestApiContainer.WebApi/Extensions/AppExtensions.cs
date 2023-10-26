using AspNetRestApiContainer.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace AspNetRestApiContainer.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}