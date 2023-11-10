using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList
{
    public partial class GetFriendsListRequest : IRequest<FriendInfo>
    {
        public required int ProfileId { get; set; } = default!;
    }
}
