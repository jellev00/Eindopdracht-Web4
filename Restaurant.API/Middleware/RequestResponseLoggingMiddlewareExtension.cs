using System.Runtime.CompilerServices;

namespace Restaurant.API.Middleware
{
    public static class RequestResponseLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestResponseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
