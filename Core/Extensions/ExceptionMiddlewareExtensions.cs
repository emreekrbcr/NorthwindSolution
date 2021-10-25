using Core.CrossCuttingConcerns.ExceptionHandling;
using Microsoft.AspNetCore.Builder;

namespace Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder app)
        { 
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}