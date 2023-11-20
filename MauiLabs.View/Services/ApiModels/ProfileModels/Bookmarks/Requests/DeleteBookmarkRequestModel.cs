using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные об удаляемой заметки рецепта
    /// </summary>
    public partial class DeleteBookmarkByIdRequestModel : DeleteBookmarkRequestModel, IMappingTarget<DeleteBookmarkCommand>
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
    public partial class DeleteBookmarkRequestModel : IMappingTarget<DeleteBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор удаляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор удаляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
