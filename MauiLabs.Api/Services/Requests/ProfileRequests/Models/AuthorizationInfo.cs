namespace MauiLabs.Api.Services.Requests.ProfileRequests.Models
{
    public sealed class AuthorizationInfo : object
    {
        public int Id { get; set; } = default!;
        public bool IsAdmin { get; set; } = default!;
    }
}
