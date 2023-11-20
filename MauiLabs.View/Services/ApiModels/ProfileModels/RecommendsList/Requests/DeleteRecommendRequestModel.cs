using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.RecommendsList.Requests
{
    /// <summary>
    /// Данные для удаления рекомендации рецепта
    /// </summary>
    public partial class DeleteRecommendRequestModel : object
    {
        /// <summary>
        /// Идентификатор записи рекомендации
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор записи рекомендации")]
        public required int RecordId { get; set; } = default!;
    }
}
