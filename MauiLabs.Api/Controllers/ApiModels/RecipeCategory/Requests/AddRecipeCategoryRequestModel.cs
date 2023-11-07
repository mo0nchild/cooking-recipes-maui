using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory;

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
        public required string Name { get; set; } = default!;
    }
}
