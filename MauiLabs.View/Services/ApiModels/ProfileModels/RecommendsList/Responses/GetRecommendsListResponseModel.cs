using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.RecommendsList.Responses
{
    /// <summary>
    /// Список рекомендаций рецептов пользователя
    /// </summary>
    public partial class GetRecommendsListResponseModel : object
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
