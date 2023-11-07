using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Requests
{
    /// <summary>
    /// Данные для реактирования профиля пользователя 
    /// </summary>
    public partial class EditProfileRequestModel : IMappingTarget<EditProfileCommand>
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Name { get; set; } = default!;

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public required string Surname { get; set; } = default!;

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public required string Email { get; set; } = default!;

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string? Phone { get; set; } = default!;

        /// <summary>
        /// Изображение профиля пользователя
        /// </summary>
        public byte[]? Image { get; set; } = default!;
    }
}
