using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.Models
{
    public partial class ProfileInfo : IMappingTarget<UserProfile>
    {
        public required int Id { get; set; } = default;
        public required string Surname { get; set; } = default!;
        public required string Name { get; set; } = default!;

        public required string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;
    }
}
