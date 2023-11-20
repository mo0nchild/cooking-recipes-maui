using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.IngredientUnits.Requests
{
    /// <summary>
    /// Данные для добавления единицы измерения
    /// </summary>
    public partial class AddIngredientUnitRequestModel : object
    {
        /// <summary>
        /// Название единицы измерения
        /// </summary>
        [StringLength(20, ErrorMessage = "Длина названия единицы измерения в диапазоне до 20 символов")]
        [Required(ErrorMessage = "Необходимо указать название единицы измерения")]
        public required string Name { get; set; } = default!;
    }
}
