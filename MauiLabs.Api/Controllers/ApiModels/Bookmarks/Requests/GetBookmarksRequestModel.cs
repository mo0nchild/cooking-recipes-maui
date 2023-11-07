using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные для получения списка заметок рецептов
    /// </summary>
    public partial class GetBookmarksRequestModel : IMappingTarget<GetBookmarksListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя, у которого ищутся заметки
        /// </summary>
        public required int ProfileId { get; set; } = default!;

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
