namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class BookmarksList : object
    {
        public required List<BookmarkInfo> Bookmarks { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
