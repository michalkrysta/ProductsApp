using Microsoft.AspNetCore.Builder;

namespace ProductsApp.Api.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }
    }
}