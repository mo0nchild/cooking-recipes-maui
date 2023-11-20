using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.RecommendsList.Requests
{
    /// <summary>
    /// Данные для получения списка рекомендаций пользователя
    /// </summary>
    public partial class GetRecommendsListRequestModel : IMappingTarget<GetRecommendsListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }
}
