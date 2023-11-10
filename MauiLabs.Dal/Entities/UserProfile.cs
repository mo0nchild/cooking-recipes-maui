using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(UserProfileConfiguration))]
    public partial class UserProfile : object
    {
        public int Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public byte[]? Image { get; set; } = default;

        public bool IsAdmin { get; set; } = default!;
        public string ReferenceLink { get; set; } = default!;
        public virtual Authorization Authorization { get; set; } = default!;

        public virtual List<Bookmark> Bookmarks { get; set; } = new();
        public virtual List<Comment> Comments { get; set; } = new();
        public virtual List<CookingRecipe> Recipes { get; set; } = new();
    }
}
