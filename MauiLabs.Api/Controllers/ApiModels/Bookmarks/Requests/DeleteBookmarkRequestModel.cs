using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark;

namespace MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные об удаляемой заметки рецепта
    /// </summary>
    public partial class DeleteBookmarkRequestModel : IMappingTarget<DeleteBookmarkCommand>
    {
        /// <summary>
        /// Идентификатор пользователя, от которого происходит удаление
        /// </summary>
        public required int ProfileId { get; set; } = default!;

        /// <summary>
        /// Идентификатор удаляемого рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
