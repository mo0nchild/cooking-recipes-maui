namespace MauiLabs.Api.Services.Requests.FriendRequests.Models
{
    public partial class FriendInfoList : object
    {
        public required List<FriendInfo> Friends { get; set; } = new();
        public required int AllCount { get; set; } = default;
    }
}
