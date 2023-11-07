namespace MauiLabs.Api.Services.Requests.CommentRequests.Models
{
    public partial class CommentsList : object
    {
        public required List<CommentInfo> Comments { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
