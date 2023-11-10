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

            builder.Property(x => x.Name).HasMaxLength(50);

            builder.HasOne(x => x.IngredientUnit).WithMany(x => x.IngredientsLists)
                .HasForeignKey(x => x.IngredientUnitId).OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.CookingRecipe).WithMany(x => x.Ingredients)
                .HasForeignKey(x => x.CookingRecipeId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
