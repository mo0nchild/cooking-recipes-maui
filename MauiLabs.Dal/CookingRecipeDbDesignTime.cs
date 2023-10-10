using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal
{
    internal class CookingRecipeDbDesignTime : IDesignTimeDbContextFactory<CookingRecipeDbContext>
    {
        public CookingRecipeDbDesignTime() : base() { }

        public CookingRecipeDbContext CreateDbContext(string[] args)
        {
            var contextOptions = new DbContextOptionsBuilder<CookingRecipeDbContext>();
            contextOptions.UseNpgsql("Server=localhost;Port=5432;Username=postgres;Password=prolodgy778;Database=cookingrecipesdb");

            return new CookingRecipeDbContext(contextOptions.Options);
        }
    }
}
