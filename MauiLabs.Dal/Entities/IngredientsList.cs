using MauiLabs.Dal.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [EntityTypeConfiguration(typeof(IngredientsListConfiguration))]
    public partial class IngredientsList : object
    {
        public int Id { get; set; } = default!;
        public double Value { get; set; } = default!;
        public string Name { get; set; } = string.Empty;

        public int IngredientUnitId { get; set; } = default!;
        public virtual IngredientUnit IngredientUnit { get; set; } = default!;

        public int CookingRecipeId { get; set; } = default!;
        public virtual CookingRecipe CookingRecipe { get; set; } = default!;
    }
}
