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
    internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable(nameof(Comment), "public", prop =>
            {
                prop.HasCheckConstraint("Rating_Constraint", "\"Rating\" BETWEEN 0 AND 5");
            });
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.Text).HasMaxLength(200).IsRequired(false);

            builder.HasOne(x => x.Profile).WithMany(x => x.Comments)
                .HasForeignKey(x => x.ProfileId)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Recipe).WithMany(x => x.Comments)
                .HasForeignKey(x => x.RecipeId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
