using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests
{
    /// <summary>
    /// Данные для получения списка друзей пользователя
    /// </summary>
    public partial class GetFriendsListRequestModel : IMappingTarget<GetFriendsListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }
}
