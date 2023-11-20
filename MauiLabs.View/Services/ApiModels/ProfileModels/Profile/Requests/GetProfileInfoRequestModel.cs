using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests
{
    /// <summary>
    /// Данные для получения информации профиля пользователя
    /// </summary>
    public partial class GetProfileInfoRequestModel : object
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int Id { get; set; } = default!;
    }
}
