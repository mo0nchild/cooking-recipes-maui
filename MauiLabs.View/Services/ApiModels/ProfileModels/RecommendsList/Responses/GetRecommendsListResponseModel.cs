using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.RecommendsList.Responses
{
    /// <summary>
    /// Список рекомендаций рецептов пользователя
    /// </summary>
    public partial class GetRecommendsListResponseModel : IMappingTarget<RecommendInfoList>
    {
        /// <summary>
        /// Данные рекомендаций рецептов в форме списка
        /// </summary>
        public required List<RecommendInfoModel> Recommends { get; set; } = new();

        /// <summary>
        /// Общее количество записей рекомендаций
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
