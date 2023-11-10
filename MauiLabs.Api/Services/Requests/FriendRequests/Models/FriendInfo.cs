using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.FriendRequests.Models
{
    public partial class FriendInfo : IMappingTarget<FriendList>
    {
        public required int Id { get; set; } = default!;
        public required DateTime DateTime { get; set; } = default!;

        public required ProfileInfo Profile { get; set; } = default!;
    }
}
