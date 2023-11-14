using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Services.Commands.FriendCommands.AddFriend;
using MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.FriendsList
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class FriendsListServiceCommands(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : FriendsListCommands.FriendsListCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }
        public bool ProfileIsAdmin { get => this._contextAccessor.HttpContext!.User.IsInRole("Admin")!; }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public override async Task<Empty> AddFriend(AddFriendModel request, ServerCallContext context)
        {
            try { await this._mediator.Send(this._mapper.Map<AddFriendCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        public override async Task<Empty> AddFriendByToken(AddFriendByTokenModel request, ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<AddFriendCommand>(request);
            mappedRequest.RequesterId = this.ProfileId;

            try { await this._mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        public override async Task<Empty> DeleteFriend(DeleteFriendModel request, ServerCallContext context)
        {
            var userFilter = (FriendInfo info) => info.Id == request.RecordId;
            try {
                var friends = await this._mediator.Send(new GetFriendsListRequest() { ProfileId = this.ProfileId });
                if (!this.ProfileIsAdmin && friends.Friends.FirstOrDefault(p => userFilter.Invoke(p)) == null)
                {
                    throw new ValidationException("Запись о друге не принадлежит пользователю");
                }
                await this._mediator.Send(this._mapper.Map<DeleteFriendCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }
    }
}
