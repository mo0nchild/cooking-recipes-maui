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
    internal sealed class BookmarkConfiguration : IEntityTypeConfiguration<Bookmark>
    {
        public void Configure(EntityTypeBuilder<Bookmark> builder)
        {
            builder.ToTable(nameof(Bookmark), "public");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Profile).WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.ProfileId)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Recipe).WithMany(x => x.Bookmarks)
                .HasForeignKey(x => x.RecipeId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
