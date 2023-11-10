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
    internal sealed class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder.ToTable(nameof(Recommendation), "public");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Text).HasMaxLength(200);

            builder.HasOne(x => x.FromUser).WithMany()
                .HasForeignKey(x => x.FromUserId).HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.ToUser).WithMany()
                .HasForeignKey(x => x.ToUserId).HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Recipe).WithMany()
                .HasForeignKey(x => x.RecipeId).HasPrincipalKey(x => x.Id);
        }
    }
}
