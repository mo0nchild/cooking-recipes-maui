using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList
{
    public enum CommentSortingType : sbyte { ByDate, ByRating }
    public partial class GetRecipeCommentsListRequest : IRequest<CommentsList>
    {
        public required int RecipeId { get; set; } = default!;
        public required int Skip { get; set; } = default!;
        public required int Take { get; set; } = default!;

        public CommentSortingType SortingType { get; set; } = default!;
    }
}
