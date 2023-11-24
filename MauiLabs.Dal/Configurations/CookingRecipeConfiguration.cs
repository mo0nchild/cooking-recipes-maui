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
    internal sealed class CookingRecipeConfiguration : IEntityTypeConfiguration<CookingRecipe>
    {
        public void Configure(EntityTypeBuilder<CookingRecipe> builder)
        {
            builder.ToTable(nameof(CookingRecipe), "public");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnType("text").IsRequired(false);

            builder.HasOne(x => x.RecipeCategory).WithMany(x => x.Recipes)
                .HasForeignKey(x => x.RecipeCategoryId).OnDelete(DeleteBehavior.Cascade)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Publisher).WithMany(x => x.Recipes)
                .HasForeignKey(x => x.PublisherId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
