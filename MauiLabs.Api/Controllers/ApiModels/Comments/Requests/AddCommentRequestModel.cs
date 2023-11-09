using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CommentCommands.AddComment;
using System.ComponentModel.DataAnnotations;

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
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Длина текст комментария в диапазоне от 5 до 200 символов")]
        [Required(ErrorMessage = "Необходимо указать текст комментария")]
        public required string Text { get; set; } = string.Empty;

        /// <summary>
        /// Значение рейтинга
        /// </summary>
        [Range(0, 5, ErrorMessage = "Значение рейтинга в диапазоне от 0 до 5")]
        [Required(ErrorMessage = "Необходимо указать значение рейтинга")]
        public required double Rating { get; set; } = default!;

        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;

        /// <summary>
        /// Идентификатор пользователя, который оставил комментарий
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }
}
