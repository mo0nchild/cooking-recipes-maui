using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Responses
{
    /// <summary>
    /// Список заметок рецептов пользователя
    /// </summary>
    public partial class GetBookmarksResponseModel : IMappingTarget<GetBookmarksListRequest>
    {
        /// <summary>
        /// Данные о заметках в форме списка
        /// </summary>
        public required List<CookingRecipeInfoModel> Recipes { get; set; } = new();
        
        /// <summary>
        /// Общее количество заметок рецептов
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
