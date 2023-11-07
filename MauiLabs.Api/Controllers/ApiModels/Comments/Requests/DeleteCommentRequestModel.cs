using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CommentCommands.DeleteComment;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для удаления рецепта
    /// </summary>
    public partial class DeleteCommentRequestModel : IMappingTarget<DeleteCommentCommand>
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;

        /// <summary>
        /// Идентификатор профиля, который добавил комментарий
        /// </summary>
        public required int ProfileId { get; set; } = default!;
    }
}
