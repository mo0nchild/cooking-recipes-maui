using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные для получения списка заметок с использованием токена
    /// </summary>
    public partial class GetBookmarksByTokenRequestModel : IMappingTarget<GetBookmarksListRequest>
    {
        /// <summary>
        /// Фильтр по названию рецепта
        /// </summary>
        public string? TextFilter { get; set; } = default;

        /// <summary>
        /// Порядок сортировки по дате добавления
        /// </summary>
        public bool ReverseOrder { get; set; } = default!;
    }
}
