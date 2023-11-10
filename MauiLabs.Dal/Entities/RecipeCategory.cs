using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [Table(nameof(RecipeCategory), Schema = "public")]
    public partial class RecipeCategory : object
    {
        public int Id { get; set; } = default;

        [MaxLength(50, ErrorMessage = "Неверное значение названия категории")]
        public string Name { get; set; } = string.Empty;

        public virtual List<CookingRecipe> Recipes { get; set; } = new();
    }
}
