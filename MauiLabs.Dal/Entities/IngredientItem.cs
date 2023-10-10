using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [Table(nameof(IngredientItem))]
    public partial class IngredientItem : object
    {
        public int Id { get; set; } = default!;

        [MaxLength(50, ErrorMessage = "Неверное значение название ингредиента")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(20, ErrorMessage = "Неверное значение название единицы измерения")]
        public string Unit { get;set; } = string.Empty;
        public virtual List<IngredientsList> IngredientsLists { get; set; } = new();
    }
}
