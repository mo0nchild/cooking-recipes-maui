using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные о добавляемой заметки рецепта при помощи токена
    /// </summary>
    public partial class AddBookmarkByTokenRequestModel : IMappingTarget<AddBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор добавляемого рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор добавляемого рецепта")]
        public required int RecipeId { get; set; } = default!;
    }
}
