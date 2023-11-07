using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.GetComment;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для получения информации о комментарии при помощи токена
    /// </summary>
    public partial class GetCommentByTokenRequestModel : IMappingTarget<GetCommentRequest>
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
