using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Responses
{
    /// <summary>
    /// Список друзей пользователя
    /// </summary>
    public partial class GetFriendsListResponseModel : object
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
