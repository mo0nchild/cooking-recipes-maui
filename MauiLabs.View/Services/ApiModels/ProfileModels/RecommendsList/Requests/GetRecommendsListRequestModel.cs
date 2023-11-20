using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.RecommendsList.Requests
{
    /// <summary>
    /// Данные для получения списка рекомендаций пользователя
    /// </summary>
    public partial class GetRecommendsListRequestModel : object
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }
}
