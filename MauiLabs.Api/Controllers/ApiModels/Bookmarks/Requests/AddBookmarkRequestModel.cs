using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные о добавляемой заметки рецепта
    /// </summary>
    public partial class AddBookmarkRequestModel : IMappingTarget<AddBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор пользователя, к которому происходит добавление
        /// </summary>
        public required int ProfileId { get; set; } = default!;

        /// <summary>
        /// Идентификатор добавляемого рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
