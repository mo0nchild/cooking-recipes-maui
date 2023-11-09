using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные об удаляемой заметки рецепта при помощи токена
    /// </summary>
    public partial class DeleteBookmarkByTokenRequestModel : IMappingTarget<DeleteBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор удаляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор удаляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
