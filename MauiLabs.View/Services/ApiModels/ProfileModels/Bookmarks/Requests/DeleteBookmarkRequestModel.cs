using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные об удаляемой заметки рецепта
    /// </summary>
    public partial class DeleteBookmarkByIdRequestModel : DeleteBookmarkRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя, от которого происходит удаление
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }

    /// <summary>
    /// Данные об удаляемой заметки рецепта при помощи токена
    /// </summary>
    public partial class DeleteBookmarkRequestModel : object
    {
        /// <summary>
        /// Идентификатор удаляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор удаляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
