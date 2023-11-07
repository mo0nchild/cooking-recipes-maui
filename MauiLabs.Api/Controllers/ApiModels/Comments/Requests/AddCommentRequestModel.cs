using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CommentCommands.AddComment;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для добавления комментария к рецепту
    /// </summary>
    public partial class AddCommentRequestModel : IMappingTarget<AddCommentCommand>
    {
        /// <summary>
        /// Текст комментария (описание)
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
