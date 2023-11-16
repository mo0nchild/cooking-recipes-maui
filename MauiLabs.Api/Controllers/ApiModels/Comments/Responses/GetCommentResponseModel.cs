using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons.ProfileModels;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Responses
{
    /// <summary>
    /// Информация о комментарии рецепта
    /// </summary>
    public partial class GetCommentResponseModel : IMappingTarget<CommentInfo>
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Текст комментария
        /// </summary>
        public required string Text { get; set; } = string.Empty;

        /// <summary>
        /// Значение рейтинга
        /// </summary>
        public required double Rating { get; set; } = default!;

        /// <summary>
        /// Дата и время добавления комментария
        /// </summary>
        public required DateTime PublicationTime { get; set; } = default;

        /// <summary>
        /// Данные пользователя, который опубликовал комментарий
        /// </summary>
        public required ProfileInfoModel Profile { get; set; } = default!;

        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
