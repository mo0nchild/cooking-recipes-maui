using MediatR;

namespace MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark
{
    public partial class DeleteBookmarkCommand : IRequest
    {
        public required int ProfileId { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;
    }
}
