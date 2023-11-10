using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Requests
{
    /// <summary>
    /// Данные для реактирования профиля пользователя 
    /// </summary>
    public partial class EditProfileRequestModel : IMappingTarget<EditProfileCommand>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int Id { get; set; } = default!;

        /// <summary> 
        /// Имя пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина имени пользователя в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать имя пользователя")]
        public string Name { get; set; } = default!;

        /// <summary> 
        /// Фамилия пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Длина фамилии пользователя в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать фамилию пользователя")]
        public string Surname { get; set; } = default!;

        /// <summary> 
        /// Данные электронной почты
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Неверный формат почты")]

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Длина почты в диапазоне от 5 до 100 символов")]
        [Required(ErrorMessage = "Не установлено значение электронной почты"), DefaultValue("string")]
        public string Email { get; set; } = default!;

        /// <summary> 
        /// Изображение в профиле пользователя
        /// </summary>
        [DefaultValue(null)]
        public byte[]? Image { get; set; } = null;
    }
}
