using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace MauiLabs.Api.RemoteServices.Implementations.FriendsList
{
    public partial class FriendsListServiceCommands(IMediator mediator, IMapper mapper)
        : FriendsListCommands.FriendsListCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        public override Task<Empty> AddFriend(AddFriendModel request, ServerCallContext context)
        {
            return base.AddFriend(request, context);
        }

        public override Task<Empty> AddFriendByToken(AddFriendByTokenModel request, ServerCallContext context)
        {
            return base.AddFriendByToken(request, context);
        }

        public override Task<Empty> DeleteFriend(DeleteFriendModel request, ServerCallContext context)
        {
            return base.DeleteFriend(request, context);
        }
    }
}
