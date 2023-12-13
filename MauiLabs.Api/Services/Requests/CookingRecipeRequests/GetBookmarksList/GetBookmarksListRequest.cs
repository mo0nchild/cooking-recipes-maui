using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList
{
    public partial class GetBookmarksListRequest : IRequest<BookmarksList>
    {
        public required int ProfileId { get; set; } = default!;

        public string? TextFilter { get; set; } = default;
        public bool ReverseOrder { get; set; } = default!;
        public string? Category { get; set; } = default!;
    }
}
