using AutoMapper;
using Grpc.Core;
using MediatR;

namespace MauiLabs.Api.RemoteServices.Implementations.FriendsList
{
    public partial class FriendsListServiceRequests(IMediator mediator, IMapper mapper)
        : FriendsListRequests.FriendsListRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        public override Task<FriendInfoListModel> GetFriendsList(GetFriendsListModel request, ServerCallContext context)
        {
            return base.GetFriendsList(request, context);
        }

        public override Task<FriendInfoListModel> GetFriendsListByToken(GetFriendsListByTokenModel request, ServerCallContext context)
        {
            return base.GetFriendsListByToken(request, context);
        }
    }
}
