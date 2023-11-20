using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.View.Services.ApiModels.Commons.ProfileModels
{
    /// <summary>
    /// Информация о профиле пользователя
    /// </summary>
    public partial class ProfileInfoModel : IMappingTarget<ProfileInfo>
    {
        /// <summary>
        /// Идентицикатор пользователя
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
        /// Изображение профиля пользователя
        /// </summary>
        public byte[] Image { get; set; } = default!;
    }
}
