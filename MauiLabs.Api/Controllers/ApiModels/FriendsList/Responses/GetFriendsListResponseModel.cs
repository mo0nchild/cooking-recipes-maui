using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons.ProfileModels;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.FriendsList.Responses
{
    /// <summary>
    /// Список друзей пользователя
    /// </summary>
    public partial class GetFriendsListResponseModel : IMappingTarget<FriendInfoList>
    {
        /// <summary>
        /// Данные о друзьях пользователя в форме списка
        /// </summary>
        public required List<FriendInfoModel> Friends { get; set; } = new();
        
        /// <summary>
        /// Общее количество записей
        /// </summary>
        public required int AllCount { get; set; } = default;
    }
}
