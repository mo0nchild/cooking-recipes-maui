using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.CommentCommands.AddComment
{
    public partial class AddCommentCommand : IRequest, IMappingTarget<Comment>
    {
        public required string Text { get; set; } = string.Empty;
        public required double Rating { get; set; } = default!;

        public required int ProfileId { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;
    }
}
