namespace MauiLabs.Api.Services.Requests.RecommendRequests.Models
{
    public partial class RecommendInfoList : object
    {
        public required List<RecommendInfo> Recommends { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
