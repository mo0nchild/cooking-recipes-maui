using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests
{
    /// <summary>
    /// Данные для удаления рецепта
    /// </summary>
    public partial class DeleteCommentByIdRequestModel : DeleteCommentRequestModel
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
    public partial class DeleteCommentRequestModel : object
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
