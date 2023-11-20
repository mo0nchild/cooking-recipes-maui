using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientUnit;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.IngredientUnits.Requests
{
    /// <summary>
    /// Данные для удаления единицы измерения
    /// </summary>
    public partial class DeleteIngredientUnitRequestModel : IMappingTarget<DeleteIngredientUnitCommand>
    {
        /// <summary>
        /// Название единицы измерения
        /// </summary>
        [StringLength(50, ErrorMessage = "Длина названия единицы измерения до 20 символов")]
        [Required(ErrorMessage = "Необходимо указать название единицы измерения")]
        public required string Name { get; set; } = default!;
    }
}
