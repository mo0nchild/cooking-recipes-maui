using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;

namespace MauiLabs.View.Services.ApiModels.Commons.ProfileModels
{
    /// <summary>
    /// Информация о заметке кулинарного рецепта
    /// </summary>
    public partial class BookmarkInfoModel : object
    {
        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Дата добавления заметки
        /// </summary>
        public required DateTime AddTime { get; set; } = default!;

        /// <summary>
        /// Данные рецепта, который добавлен как заметка
        /// </summary>
        public required GetRecipeResponseModel Recipe { get; set; } = default!;
    }
}
