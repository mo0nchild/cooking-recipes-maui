using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class PublisherInfo : IMappingTarget<UserProfile>
    {
        public required string Name { get; set; } = default!;
        public required string Surname { get; set; } = default!;
        public byte[]? Image { get; set; } = default;
    }
}
