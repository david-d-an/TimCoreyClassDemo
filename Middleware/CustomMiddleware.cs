using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Middleware {
    public class CustomMiddleware {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            var id = context.Request.Query["id"];
            if (!string.IsNullOrWhiteSpace(id)) {
            }

            await _next(context);
        }
    }

    public static class CustomMiddlewareExtensions {
        public static IApplicationBuilder UseCustomMiddlewareFeatures(this IApplicationBuilder builder) {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
