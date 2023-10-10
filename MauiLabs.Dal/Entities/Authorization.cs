using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(AuthorizationCofiguration))]
    public partial class Authorization : object
    {
        public int Id { get; set; } = default;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int UserProfileId { get; set; } = default!;
        public virtual UserProfile UserProfile { get; set; } = default!;
    }
}
