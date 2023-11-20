using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests
{
    /// <summary>
    /// Данные для получения информации о комментарии
    /// </summary>
    public partial class GetCommentByIdRequestModel : GetCommentRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя, который опубликовал комментарий
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }

    /// <summary>
    /// Данные для получения информации о комментарии при помощи токена
    /// </summary>
    public partial class GetCommentRequestModel : object
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
