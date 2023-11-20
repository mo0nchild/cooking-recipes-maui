using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные о добавляемой заметки рецепта
    /// </summary>
    public partial class AddBookmarkByIdRequestModel : AddBookmarkRequestModel, IMappingTarget<AddBookmarkCommand>
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
    public partial class AddBookmarkRequestModel : IMappingTarget<AddBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор добавляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор добавляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
