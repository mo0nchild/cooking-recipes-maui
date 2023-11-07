using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Responses
{
    /// <summary>
    /// Данные профиля пользователя
    /// </summary>
    public partial class GetProfileInfoResponseModel : IMappingTarget<ProfileInfo>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required int Id { get; set; } = default;
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; } = default!;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; } = default!;

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
