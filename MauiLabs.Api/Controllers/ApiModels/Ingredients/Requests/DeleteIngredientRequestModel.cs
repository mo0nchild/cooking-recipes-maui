using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientItem;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Requests
{
    /// <summary>
    /// Данные для удаления ингредиента
    /// </summary>
    public partial class DeleteIngredientRequestModel : IMappingTarget<DeleteIngredientItemCommand>
    {
        /// <summary>
        /// Название ингредиента
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина названия ингредиента в диапазоне от 3 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать название ингредиента")]
        public required string Name { get; set; } = default!;
    }
}
