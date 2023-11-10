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
    internal sealed class FriendListConfiguration : IEntityTypeConfiguration<FriendList>
    {
        public void Configure(EntityTypeBuilder<FriendList> builder)
        {
            builder.ToTable(nameof(FriendList), "public");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.HasOne(x => x.Requester).WithMany()
                .HasForeignKey(x => x.RequesterId).HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Addressee).WithMany()
                .HasForeignKey(x => x.AddresseeId).HasPrincipalKey(x => x.Id);
        }
    }
}
