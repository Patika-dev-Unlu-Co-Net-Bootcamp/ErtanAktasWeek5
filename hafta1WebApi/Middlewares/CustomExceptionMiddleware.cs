
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace hafta1WebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                string message = "[Request] HTTP" + context.Request.Method + " - " + context.Request.Path;
                Console.WriteLine(message);
                await _next(context);
                message = "Response " + context.Request.Method + "  " + context.Response.StatusCode;
                Console.WriteLine(message);
            }catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string message = "We across an Error in "+ context.Request.Method + " reason is :" + ex.Message +"status Code"+ context.Response.StatusCode;
            Console.WriteLine(message);

            context.Response.ContentType = "application/json";
            

            var result = JsonConvert.SerializeObject(new {error = ex.Message}, Formatting.None);

            return context.Response.WriteAsync(result);

        }
    }
    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMidde(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
