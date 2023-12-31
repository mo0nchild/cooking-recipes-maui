using Calzolari.Grpc.AspNetCore.Validation;
using MauiLabs.Api.Commons.Authentication;
using MauiLabs.Api.Commons.ConfigureOptions;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.RemoteServices;
using MauiLabs.Api.RemoteServices.Implementations.CookingRecipe;
using MauiLabs.Api.RemoteServices.Implementations.FriendsList;
using MauiLabs.Api.RemoteServices.Implementations.RecommendsList;
using MauiLabs.Api.Services;
using MauiLabs.Dal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using static MauiLabs.Api.Commons.Authentication.ConfigureJwtBearer;

namespace MauiLabs.Api;
using JwtBearerConfig = ConfigureJwtBearer.JwtBearerConfig;
public static class Program : object
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();

        //builder.Services.Configure<JwtBearerConfig>(builder.Configuration.GetSection("Authentication"));
        builder.Services.Configure<JwtBearerConfig>(options =>
        {
            var authOptions = builder.Configuration.GetSection("Authentication").Get<JwtBearerConfig>()!;
            options.SecretKey = Guid.NewGuid().ToString();
            (options.Issuer, options.Audience) = (authOptions.Issuer, authOptions.Audience);
        });
        builder.Services.ConfigureOptions<ConfigureJwtBearer>();
        builder.Services.ConfigureOptions<ConfigureApiAccess>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", item => item.RequireClaim(ClaimTypes.Role, "Admin"));
            options.AddPolicy("User", item => item.RequireClaim(ClaimTypes.Role, "User"));
        });
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("cookingrecipes", new OpenApiInfo()
            {
                Title = "Cooking Recipes",
                Version = "v1",
                Description = "API ��� ������ ������� ��������� ��������",
                Contact = new OpenApiContact()
                {
                    Name = "byterbrod",
                    Url = new Uri("https://github.com/mo0nchild")
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"������� JWT ����� �����������",
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
        await builder.Services.AddRemoteServices(builder.Configuration);
        await builder.Services.AddDataAccessLayer(builder.Configuration);
        await builder.Services.AddApiServices(builder.Configuration);

        var application = builder.Build();
        application.UseHttpsRedirection().UseStaticFiles();

        if (application.Environment.IsDevelopment())
        {
            application.UseSwagger();
            application.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/cookingrecipes/swagger.json", "cookingrecipes");
                options.InjectStylesheet("/styles/swagger-darkui.css");
            });
        }
        else application.UseApiAccess().UseRequestLogging();
        application.UseAuthentication().UseRouting().UseAuthorization();

        application.UseEndpoints(options => options.MapRemoteServices().MapControllers());
        await application.RunAsync();
    }
}
