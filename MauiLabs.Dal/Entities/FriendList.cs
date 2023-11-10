using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(FriendListConfiguration))]
    public partial class FriendList : object
    {
        public int Id { get; set; } = default!;
        public DateTime DateTime { get; set; } = default;

        public int RequesterId { get; set; } = default!;
        public virtual UserProfile Requester { get; set; } = default!;

        public int AddresseeId { get; set; } = default!;
        public virtual UserProfile Addressee { get; set; } = default!;
    }
}
