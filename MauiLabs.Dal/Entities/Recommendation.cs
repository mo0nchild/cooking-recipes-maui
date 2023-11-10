using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(RecommendationConfiguration))]
    public partial class Recommendation : object
    {
        public int Id { get; set; } = default!;
        public string Text { get; set; } = default!;

        public int FromUserId { get; set; } = default!;
        public virtual UserProfile FromUser { get; set; } = default!;
        public int ToUserId { get; set; } = default!;
        public virtual UserProfile ToUser { get; set; } = default!;

        public int RecipeId { get; set; } = default!;
        public virtual CookingRecipe Recipe { get; set; } = default!;
    }
}
