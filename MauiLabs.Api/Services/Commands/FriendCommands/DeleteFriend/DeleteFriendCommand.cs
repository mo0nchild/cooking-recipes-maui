using MediatR;

namespace MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend
{
    public partial class DeleteFriendCommand : IRequest
    {
        public required int RecordId { get; set; } = default!;
    }
}
