using MediatR;

namespace MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark
{
    public partial class AddBookmarkCommand : IRequest
    {
        public required int ProfileId { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;
    }
}
