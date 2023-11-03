using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MauiLabs.Api.Controllers.ApiModels.Authorization
{
    /// <summary>
    /// Данные для регистрации профиля пользователя
    /// </summary>
    public partial class RegistrationRequestModel : IMappingTarget<RegistrationCommand>, IValidatableObject
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
        /// Данные номера телефона
        /// </summary>
        [DefaultValue(null)]
        public string? Phone { get; set; } = default!;

        /// <summary> 
        /// Изображение в профиле пользователя
        /// </summary>
        [DefaultValue(null)]
        public byte[]? Image { get; set; } = null;

        /// <summary> 
        /// Логин пользователя 
        /// </summary>
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина логина в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать логин пользователя")]
        public string Login { get; set; } = default!;

        /// <summary> 
        /// Пароль пользователя 
        /// </summary>
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Длина пароля в диапазоне от 5 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать пароль пользователя")]
        public string Password { get; set; } = default!;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            if (Phone != null && !Regex.IsMatch(Phone, @"^\+7[0-9]{10}$"))
            {
                validationResult.Add(new ValidationResult("Неверный формат номера телефона", new string[] { "Phone" }));
            }
            return validationResult;
        }
    }
}
