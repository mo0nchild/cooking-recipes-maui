using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetProfileCommentsList
{
    public partial class GetProfileCommentsListRequest : IRequest<CommentsList>
    {
        public required int ProfileId { get; set; } = default!;
        public required int Skip { get; set; } = default!;
        public required int Take { get; set; } = default!;

        public CommentSortingType SortingType { get; set; } = default!;
    }
}
