using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CommentCommands.EditComment;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для изменения комментария рецепта
    /// </summary>
    public partial class EditCommentRequestModel : IMappingTarget<EditCommentCommand>
    {
        /// <summary>
        /// Текст комментария
        /// </summary>
        public required string Text { get; set; } = string.Empty;

        /// <summary>
        /// Значение рейтинга
        /// </summary>
        public required double Rating { get; set; } = default!;

        /// <summary>
        /// Идентификатор пользователя, который оставил комментарий
        /// </summary>
        public required int ProfileId { get; set; } = default!;

        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
