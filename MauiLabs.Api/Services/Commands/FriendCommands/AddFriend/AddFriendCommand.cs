using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.FriendCommands.AddFriend
{
    public partial class AddFriendCommand : IRequest
    {
        public required int RequesterId { get; set; } = default!;
        public required string ReferenceLink { get; set; } = default!;
    }
}
