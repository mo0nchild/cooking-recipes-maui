using MauiLabs.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal
{
    public partial class CookingRecipeDbContext : DbContext
    {
        public virtual DbSet<Authorization> Authorizations { get; set; } = default!;
        public virtual DbSet<Bookmark> Bookmarks{ get; set; } = default!;
        public virtual DbSet<Comment> Comments { get; set; } = default!;
        public virtual DbSet<UserProfile> UserProfiles { get; set; } = default!;

        public virtual DbSet<CookingRecipe> CookingRecipes { get; set; } = default!;
        public virtual DbSet<IngredientsList> Ingredients { get; set; } = default!;
        public virtual DbSet<IngredientUnit> IngredientUnits { get; set; } = default!;
        public virtual DbSet<RecipeCategory> RecipeCategories { get; set; } = default!;

        public virtual DbSet<LoggingInfo> LoggingInfos { get; set; } = default!;
        public virtual DbSet<FriendList> FriendsList { get; set; } = default!;
        public virtual DbSet<Recommendation> Recommendations { get; set; } = default!;

        public CookingRecipeDbContext(DbContextOptions<CookingRecipeDbContext> options) : base(options) { }

        public CookingRecipeDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseLazyLoadingProxies());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
