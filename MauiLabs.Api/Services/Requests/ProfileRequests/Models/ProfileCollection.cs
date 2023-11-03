namespace MauiLabs.Api.Services.Requests.ProfileRequests.Models
{
    public partial class ProfileCollection : object
    {
        public List<ProfileInfo> Profiles { get; set; } = new();
        public int AllCount { get; set; } = default!;
    }
}
