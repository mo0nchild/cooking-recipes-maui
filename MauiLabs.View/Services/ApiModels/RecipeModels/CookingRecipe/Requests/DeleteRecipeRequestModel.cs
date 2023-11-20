using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для удаления кулинарного рецепта
    /// </summary>
    public partial class DeleteRecipeRequestModel : IMappingTarget<DeleteCookingRecipeCommand>
    {
        /// <summary>
        /// Идентификатор кулинарного рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор кулинарного рецепта")]
        public required int Id { get; set; } = default!;
    }
}
