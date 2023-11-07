using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(CookingRecipeConfiguration))]
    public partial class CookingRecipe : object
    {
        public int Id { get; set; } = default!;

        public DateTime PublicationTime { get; set; } = default!;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

        public bool Confirmed { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public int? RecipeCategoryId { get; set; } = default!;
        public virtual RecipeCategory? RecipeCategory { get; set; } = default!;
        public int PublisherId { get; set; } = default!;
        public virtual UserProfile Publisher { get; set; } = default!;

        public virtual List<Comment> Comments { get; set; } = new();
        public virtual List<Bookmark> Bookmarks { get; set; } = new();
        public virtual List<IngredientsList> Ingredients { get; set; } = new();

    }
}
