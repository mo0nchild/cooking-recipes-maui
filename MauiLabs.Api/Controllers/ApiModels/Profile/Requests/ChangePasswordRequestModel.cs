using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Requests
{
    /// <summary>
    /// Данные для изменения пароля профиля пользователя
    /// </summary>
    public partial class ChangePasswordRequestModel : IMappingTarget<ChangePasswordCommand>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Старый пароль авторизации профиля
        /// </summary>
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина старого пароля в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать старый пароль")]
        public required string OldPassword { get; set; } = default!;

        /// <summary>
        /// Новый пароль авторизации профиля
        /// </summary>
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина нового пароля в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать новый пароль")]
        public required string NewPassword { get; set; } = default!;
    }
}
