using MauiLabs.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Configurations
{
    internal sealed class IngredientsListConfiguration : IEntityTypeConfiguration<IngredientsList>
    {
        public void Configure(EntityTypeBuilder<IngredientsList> builder)
        {
            builder.ToTable(nameof(IngredientsList), "public");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.IngredientItem).WithMany(x => x.IngredientsLists)
                .HasForeignKey(x => x.IngredientItemId).OnDelete(DeleteBehavior.SetNull)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.CookingRecipe).WithMany(x => x.Ingredients)
                .HasForeignKey(x => x.CookingRecipeId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
