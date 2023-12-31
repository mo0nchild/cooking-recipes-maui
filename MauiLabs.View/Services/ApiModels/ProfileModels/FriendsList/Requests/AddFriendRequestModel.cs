﻿using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests
{
    /// <summary>
    /// Данные для добавления пользователя в друзья
    /// </summary>
    public partial class AddFriendByIdRequestModel : AddFriendRequestModel
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
    public partial class AddFriendRequestModel : object
    {
        /// <summary>
        /// Ссылка на профиль друга
        /// </summary>
        [StringLength(72, MinimumLength = 5, ErrorMessage = "Длина ссылки в диапазоне от 5 до 72 символов")]
        [Required(ErrorMessage = "Необходимо указать текст ссылки на профиль")]
        public required string ReferenceLink { get; set; } = default!;
    }
}
