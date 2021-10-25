using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.ExceptionHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext) //Asenkron olmasının sebebi ASP.Net yaşam döngüsündeki middleware'lerin asenkron yapıda olması
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            if (e.GetType() == typeof(ValidationException)) //Burayı farklı hatalar için daha da güzelleştirebiliriz
            {
                message = e.Message;
                var errors = ((ValidationException)e).Errors;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = message,
                    Errors = errors
                }.ToString());
            }

            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}