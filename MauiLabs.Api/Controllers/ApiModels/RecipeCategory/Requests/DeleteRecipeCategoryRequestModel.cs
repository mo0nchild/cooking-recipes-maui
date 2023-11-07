using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecipeCategoryCommands.DeleteRecipeCategory;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeCategory.Requests
{
    /// <summary>
    /// Данные для удаления категории рецептов
    /// </summary>
    public partial class DeleteRecipeCategoryRequestModel : IMappingTarget<DeleteRecipeCategoryCommand>
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public required string Name { get; set; } = default!;
    }
}
