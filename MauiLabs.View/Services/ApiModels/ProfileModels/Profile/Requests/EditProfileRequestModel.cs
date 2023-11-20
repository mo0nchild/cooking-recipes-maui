using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using System.ComponentModel.DataAnnotations;

/* Необъединенное слияние из проекта "MauiLabs.View (net7.0-windows10.0.19041.0)"
До:
using System.ComponentModel;

namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.Profile.Requests
После:
using System.ComponentModel;
using MauiLabs;
using MauiLabs.Api;
using MauiLabs.Api.Controllers;
using MauiLabs.Api.Controllers.ApiModels;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.Profile;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile
*/

/* Необъединенное слияние из проекта "MauiLabs.View (net7.0-windows10.0.19041.0)"
До:
using System.ComponentModel;
После:
using System.ComponentModel;
using MauiLabs;
using MauiLabs.View;
using MauiLabs.View.Services;
using MauiLabs.View.Services.ApiModels;
using MauiLabs.View.Services.ApiModels.ProfileModels;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
*/
using System.ComponentModel;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests
{
    /// <summary>
    /// Данные для реактирования профиля пользователя 
    /// </summary>
    public partial class EditProfileByIdRequestModel : EditProfileRequestModel, IMappingTarget<EditProfileCommand>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int Id { get; set; } = default!;
    }

    /// <summary>
    /// Данные для реактирования профиля пользователя при помощи токена 
    /// </summary>
    public partial class EditProfileRequestModel : IMappingTarget<EditProfileCommand>
    {
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
        public byte[] Image { get; set; } = null;
    }
}
