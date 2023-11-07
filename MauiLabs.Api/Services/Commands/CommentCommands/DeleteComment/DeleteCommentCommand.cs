using MediatR;

namespace MauiLabs.Api.Services.Commands.CommentCommands.DeleteComment
{
    public partial class DeleteCommentCommand : IRequest
    {
        public required int RecipeId { get; set; } = default!;
        public required int ProfileId { get; set; } = default!;
    }
}
