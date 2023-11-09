using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для удаления комментария с использованием токена
    /// </summary>
    public partial class DeleteCommentByTokenRequestModel : object
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
