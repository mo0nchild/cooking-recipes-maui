using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для получения информации о кулинарном рецепте
    /// </summary>
    public partial class GetRecipeRequestModel : IMappingTarget<GetCookingRecipeRequest>
    {
        /// <summary>
        /// Идентификатор кулинарного рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор кулинарного рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
