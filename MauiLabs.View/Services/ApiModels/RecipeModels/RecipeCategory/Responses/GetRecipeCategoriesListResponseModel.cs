
namespace MauiLabs.View.Services.ApiModels.RecipeModels.RecipeCategory.Responses
{
    /// <summary>
    /// Список категорий рецептов
    /// </summary>
    public partial class GetRecipeCategoriesListResponseModel : object
    {
        /// <summary>
        /// Данные категорий в форме списка
        /// </summary>
        public required List<string> Categories { get; set; } = new();
    }
}
