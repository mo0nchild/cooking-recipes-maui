using MauiLabs.Api.Commons.ConfigureOptions;
using MauiLabs.Api.Services.Commons.Interfaces;
using MauiLabs.Api.Services.Commons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;

namespace MauiLabs.Api.Commons.Middleware
{
    using ApiAccessOptions = ConfigureApiAccess.ApiAccessOptions;
    public partial class ApiAccessMiddleWare : object
    {
        protected internal readonly RequestDelegate requestDelegate = default!;
        protected internal readonly ApiAccessOptions accessOptions = default!;

        public ApiAccessMiddleWare(RequestDelegate requestDelegate, IOptions<ApiAccessOptions> options) : base()
        {
            this.requestDelegate = requestDelegate;
            this.accessOptions = options.Value;
        }
        public virtual async Task InvokeAsync(HttpContext context)
        {
            var ignorePath = new List<string> { "/swagger/v1/swagger.json", "/swagger/v2/swagger.json", "/index.html" };
            if (ignorePath.Contains(context.Request.Path.Value!)) await this.requestDelegate.Invoke(context);

            if (context.Request.Headers["ApiKey"] != this.accessOptions.ApiKey)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                await Results.Problem("Не предоставлен ключ для использования функций API", 
                    statusCode: (int)HttpStatusCode.NotAcceptable).ExecuteAsync(context);
            }
            else await this.requestDelegate.Invoke(context);
        }
    }
    public static class UseApiAccessMiddleWareExtension : object
    {
        public static IApplicationBuilder UseApiAccess(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiAccessMiddleWare>();
        }
    }
}
