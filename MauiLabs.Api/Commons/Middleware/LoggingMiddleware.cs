using MauiLabs.Api.Services.Commons.Interfaces;
using MauiLabs.Api.Services.Commons.Models;
using System.Net;

namespace MauiLabs.Api.Commons.Middleware
{
    public partial class RequestLoggingMiddleware: object
    {
        private readonly RequestDelegate _requestDelegate = default!;
        private readonly IRequestLogging _requestLogging = default!;

        public RequestLoggingMiddleware(RequestDelegate requestDelegate, IRequestLogging requestLogging) : base()
        {
            this._requestDelegate = requestDelegate;
            this._requestLogging = requestLogging;
        }
        public virtual async Task InvokeAsync(HttpContext context)
        {
            await this._requestLogging.LogRequest(new LogRequestMessage()
            {
                UserInfo = (context.Connection.RemoteIpAddress ?? IPAddress.Loopback).ToString(),
                DateTime = DateTime.UtcNow, MethodName = context.Request.Path,
            });
            await this._requestDelegate.Invoke(context);
        }
    }
    public static class UseRequestLoggingMiddlewareExtension : object
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
