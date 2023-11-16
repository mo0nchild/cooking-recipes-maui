using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.Models;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Commons.RecipeModels
{
    /// <summary>
    /// Данные о единице измерения ингредиента
    /// </summary>
    public partial class IngredientUnitModel : IMappingTarget<IngredientUnitInfo>
    {
        /// <summary>
        /// Название единицы измерения
        /// </summary>
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Длина названия единицы измерения до 20 символов")]
        [Required(ErrorMessage = "Необходимо указать название единицы измерения")]
        public required string Unit { get; set; } = default!;

        /// <summary>
        /// Значение ингредиента
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать значение ингредиента")]
        public required double Value { get; set; } = default!;
    }
}
