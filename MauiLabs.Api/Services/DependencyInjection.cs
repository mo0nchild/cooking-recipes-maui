using FluentValidation;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commons.Behaviors;
using MauiLabs.Api.Services.Commons.Implementation;
using MauiLabs.Api.Services.Commons.Interfaces;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MauiLabs.Api.Services
{
    public static class DependencyInjection : object
    {
        public static Task<IServiceCollection> AddApiServices(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            collection.AddAutoMapper(options =>
            {
                options.AddProfile(new AssemblyProfile(Assembly.GetExecutingAssembly()));
            });
            collection.AddMediatR(options =>
            {
                options.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Transient);
                options.RegisterServicesFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            });
            collection.AddTransient<IRequestLogging, DatabaseRequestLogging>();
            return Task.FromResult(collection);
        }
    }
}
