using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeModels.CookingRecipe.Responses
{
    /// <summary>
    /// Список кулинарных рецептов
    /// </summary>
    public partial class GetRecipesListResponseModel : IMappingTarget<CookingRecipesList>
    {
        /// <summary>
        /// Данные о кулинарных рецептах в форме списка
        /// </summary>
        public required List<GetRecipeResponseModel> Recipes { get; set; } = new();

        /// <summary>
        /// Общее количество записей
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
