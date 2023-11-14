using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Responses
{
    /// <summary>
    /// Список заметок рецептов пользователя
    /// </summary>
    public partial class GetBookmarksResponseModel : IMappingTarget<BookmarksList>
    {
        /// <summary>
        /// Данные о заметках в форме списка
        /// </summary>
        public required List<BookmarkInfoModel> Bookmarks { get; set; } = new();

        /// <summary>
        /// Общее количество заметок рецептов
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
