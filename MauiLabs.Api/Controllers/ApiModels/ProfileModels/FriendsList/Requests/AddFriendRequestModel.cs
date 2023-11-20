using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.FriendCommands.AddFriend;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.FriendsList.Requests
{
    /// <summary>
    /// Данные для добавления пользователя в друзья
    /// </summary>
    public partial class AddFriendByIdRequestModel : AddFriendRequestModel, IMappingTarget<AddFriendCommand>
    {
        /// <summary>
        /// Идентификатор пользователя, отправителя запроса
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int RequesterId { get; set; } = default!;
    }

    /// <summary>
    /// Данные для добавления пользователя в друзья при помощи токена
    /// </summary>
    public partial class AddFriendRequestModel : IMappingTarget<AddFriendCommand>
    {
        /// <summary>
        /// Ссылка на профиль друга
        /// </summary>
        [StringLength(72, MinimumLength = 5, ErrorMessage = "Длина ссылки в диапазоне от 5 до 72 символов")]
        [Required(ErrorMessage = "Необходимо указать текст ссылки на профиль")]
        public required string ReferenceLink { get; set; } = default!;
    }
}
