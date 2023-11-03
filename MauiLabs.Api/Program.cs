using MauiLabs.Api.Commons.Authentication;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Services;
using MauiLabs.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using static MauiLabs.Api.Commons.Authentication.ConfigureJwtBearer;

namespace MauiLabs.Api;
using JwtBearerConfig = ConfigureJwtBearer.JwtBearerConfig;
public static class Program : object
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.Configure<JwtBearerConfig>(builder.Configuration.GetSection("Authentication"));
        builder.Services.ConfigureOptions<ConfigureJwtBearer>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.AddAuthorization(options => { });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("cookingrecipes", new OpenApiInfo()
            {
                Title = "Cooking Recipes",
                Version = "v1",
                Description = "API дл€ работы сервиса просмотра рецептов",
                Contact = new OpenApiContact()
                {
                    Name = "byterbrod",
                    Url = new Uri("https://github.com/mo0nchild")
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"¬ведите JWT токен авторизации",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[]{}
                }
            });
            var localpath = AppDomain.CurrentDomain.BaseDirectory;
            options.IncludeXmlComments(Path.Combine(localpath, "MauiLabs.Api.xml"));
        });
        await builder.Services.AddDataAccessLayer(builder.Configuration);
        await builder.Services.AddApiServices(builder.Configuration);

        var application = builder.Build();
        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/cookingrecipes/swagger.json", "cookingrecipes");
                options.InjectStylesheet("/styles/swagger-darkui.css");
            });
        }
        application.UseExceptionHandler();
        application.UseHttpsRedirection().UseStaticFiles();
        application.UseAuthentication().UseAuthorization();

        application.UseRequestLogging();
        application.UseRouting().UseEndpoints(options => options.MapControllers());
        await application.RunAsync();
    }
}
