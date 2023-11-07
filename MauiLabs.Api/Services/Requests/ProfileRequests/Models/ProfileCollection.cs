namespace MauiLabs.Api.Services.Requests.ProfileRequests.Models
{
    public partial class ProfileCollection : object
    {
        public required List<ProfileInfo> Profiles { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
