using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.GetComment;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для получения информации о комментарии
    /// </summary>
    public partial class GetCommentRequestModel : IMappingTarget<GetCommentRequest>
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;

        /// <summary>
        /// Идентификатор пользователя, который опубликовал комментарий
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }
}
