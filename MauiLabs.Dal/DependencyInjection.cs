using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return collection;
        }
    }
}
