using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetComment
{
    public partial class GetCommentRequest : IRequest<CommentInfo?>
    {
        public required int RecipeId { get; set; } = default!;
        public required int ProfileId { get; set; } = default!;
    }
}
