using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal
{
    public static class DependencyInjection : object
    {
        public async static Task<IServiceCollection> AddDataAccessLayer(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContextFactory<CookingRecipeDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("cookingrecipedb"));
            });
            var serviceProvider = collection.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetService<IDbContextFactory<CookingRecipeDbContext>>()!;

            using (var dbcontext = await dbcontextFactory.CreateDbContextAsync())
            {
                await dbcontext.Database.MigrateAsync();
            }
            var databaseUrlInfo = $"Database URL: {configuration.GetConnectionString("cookingrecipedb")}";
            serviceProvider.GetService<ILogger>()?.LogInformation(databaseUrlInfo);
            return collection;
        }
    }
}
