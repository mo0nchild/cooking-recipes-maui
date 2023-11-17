using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Commons.ProfileModels
{
    /// <summary>
    /// Данные рекомендации рецепта
    /// </summary>
    public partial class RecommendInfoModel : IMappingTarget<RecommendInfo>
    {
        /// <summary>
        /// Идентификатор рекомендации рецепта
        /// </summary>
        public required int Id { get; set; } = default!;
        
        /// <summary>
        /// Текст рекомендации рецепта
        /// </summary>
        public required string Text { get; set; } = default!;

        /// <summary>
        /// Данные пользователя отправителя
        /// </summary>
        public required ProfileInfoModel FromUser { get; set; } = default!;

        /// <summary>
        /// Данные пользователя получателя
        /// </summary>
        public required ProfileInfoModel ToUser { get; set; } = default!;

        /// <summary>
        /// Данные кулинарного рецепта
        /// </summary>
        public required CookingRecipeInfo Recipe { get; set; } = default!;
    }
}
