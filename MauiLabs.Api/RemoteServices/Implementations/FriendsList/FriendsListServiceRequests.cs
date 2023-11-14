using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.FriendsList
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class FriendsListServiceRequests(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : FriendsListRequests.FriendsListRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        public override async Task GetFriendsList(GetFriendsListModel request, 
            IServerStreamWriter<FriendInfoModel> responseStream, ServerCallContext context)
        {
            FriendInfoList requestResult = default!;
            try { requestResult = await this._mediator.Send(this._mapper.Map<GetFriendsListRequest>(request)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            requestResult.Friends.ForEach(async item =>
            {
                var mappedResponse = this._mapper.Map<FriendInfoModel>(item);
                await responseStream.WriteAsync(mappedResponse);
            });
        }
        public override async Task GetFriendsListByToken(GetFriendsListByTokenModel request,
            IServerStreamWriter<FriendInfoModel> responseStream, ServerCallContext context)
        {
            var mappedRequest = new GetFriendsListModel() { ProfileId = this.ProfileId };
            await this.GetFriendsList(mappedRequest, responseStream, context);
        }
    }
}
