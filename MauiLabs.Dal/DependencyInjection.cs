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
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddDbContextFactory<CookingRecipeDbContext>(options =>
            {
                var @string = configuration["ConnectionStrings:cookingrecipedb"];
                options.UseNpgsql(@string);
            });
            var serviceProvider = collection.BuildServiceProvider();
            var dbcontextFactory = serviceProvider.GetService<IDbContextFactory<CookingRecipeDbContext>>()!;

            using (var dbcontext = dbcontextFactory.CreateDbContext())
            {
                dbcontext.Database.Migrate();
            }
            return collection;
        }
    }
}
