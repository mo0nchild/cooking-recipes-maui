using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные о добавляемой заметки рецепта
    /// </summary>
    public partial class AddBookmarkByIdRequestModel : AddBookmarkRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя, к которому происходит добавление
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }

    /// <summary>
    /// Данные о добавляемой заметки рецепта при помощи токена
    /// </summary>
    public partial class AddBookmarkRequestModel : object
    {
        /// <summary>
        /// Идентификатор добавляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор добавляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
