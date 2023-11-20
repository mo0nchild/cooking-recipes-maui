using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для изменения статуса кулинарного рецепта
    /// </summary>
    public partial class ConfirmeRecipeRequestModel : IMappingTarget<ConfirmeCookingRecipeCommand>
    {
        /// <summary>
        /// Идентификатор кулинарного рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор кулинарного рецепта")]
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Новый статус записи кулинарного рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать новый статус записи рецепта")]
        public required bool Status { get; set; } = default!;
    }
}
