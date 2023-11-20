using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses
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
        public required string Surname { get; set; } = default!;

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Name { get; set; } = default!;

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public required string Email { get; set; } = default!;

        /// <summary>
        /// Ссылка для добавления в друзья
        /// </summary>
        public required string ReferenceLink { get; set; } = default!;

        /// <summary>
        /// Изображение профиля пользователя
        /// </summary>
        public byte[] Image { get; set; } = default!;
    }
}
