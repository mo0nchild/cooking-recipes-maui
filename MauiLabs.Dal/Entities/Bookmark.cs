using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(BookmarkConfiguration))]
    public partial class Bookmark : object
    {
        public int Id { get; set; } = default!;
        public DateTime AddTime { get; set; } = default!;

        public int RecipeId { get; set; } = default!;
        public virtual CookingRecipe Recipe { get; set; } = default!;

        public int ProfileId { get; set; } = default!;
        public virtual UserProfile Profile { get; set; } = default!;
    }
}
