using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CommentCommands.DeleteComment;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeModels.Comments.Requests
{
    /// <summary>
    /// Данные для удаления рецепта
    /// </summary>
    public partial class DeleteCommentByIdRequestModel : DeleteCommentRequestModel, IMappingTarget<DeleteCommentCommand>
    {
        /// <summary>
        /// Идентификатор профиля, который добавил комментарий
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор профиля")]
        public required int ProfileId { get; set; } = default!;
    }


    /// <summary>
    /// Данные для удаления комментария с использованием токена
    /// </summary>
    public partial class DeleteCommentRequestModel : IMappingTarget<DeleteCommentCommand>
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
