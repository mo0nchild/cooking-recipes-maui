
namespace MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses
{
    /// <summary>
    /// Список кулинарных рецептов
    /// </summary>
    public partial class GetRecipesListResponseModel : object
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
