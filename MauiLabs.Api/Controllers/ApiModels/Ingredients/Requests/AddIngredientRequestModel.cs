using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Requests
{
    /// <summary>
    /// Данные для добавления ингредиента
    /// </summary>
    public partial class AddIngredientRequestModel : IMappingTarget<AddIngredientItemCommand>
    {
        /// <summary>
        /// Название ингредиента
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина названия ингредиента в диапазоне от 3 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать название ингредиента")]
        public required string IngredientName { get; set; } = default!;

        /// <summary>
        /// Единицы измерения
        /// </summary>
        [StringLength(20, ErrorMessage = "Длина названия единицы измерения в диапазоне до 20 символов")]
        [Required(ErrorMessage = "Необходимо указать название единицы измерения")]
        public required string UnitName { get; set; } = default!;
    }
}
