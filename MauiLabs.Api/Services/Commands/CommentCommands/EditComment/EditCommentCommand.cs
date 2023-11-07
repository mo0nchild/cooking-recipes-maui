using MediatR;

namespace MauiLabs.Api.Services.Commands.CommentCommands.EditComment
{
    public partial class EditCommentCommand : IRequest
    {
        public required string Text { get; set; } = string.Empty;
        public required double Rating { get; set; } = default!;

        public required int ProfileId { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;
    }
}
