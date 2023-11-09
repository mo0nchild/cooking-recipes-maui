using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeCategory.Requests
{
    /// <summary>
    /// Данные для добавления категории рецептов
    /// </summary>
    public partial class AddRecipeCategoryRequestModel : IMappingTarget<AddRecipeCategoryCommand>
    {
        /// <summary>
        /// Название категории
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина названия в диапазоне от 3 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать название категории")]
        public required string Name { get; set; } = default!;
    }
}
