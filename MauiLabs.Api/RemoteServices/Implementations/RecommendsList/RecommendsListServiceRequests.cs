using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class RecommendsListServiceRequests(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : RecommendsListRequests.RecommendsListRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public override async Task GetRecommendsList(GetRecommendsListModel request, 
            IServerStreamWriter<RecommendInfoModel> responseStream, ServerCallContext context)
        {
            RecommendInfoList requestResult = default!;
            try { requestResult = await this._mediator.Send(this._mapper.Map<GetRecommendsListRequest>(request)); }
            catch (ValidationException errorInfo) 
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            requestResult.Recommends.ForEach(async item =>
            {
                var mappedResponse = this._mapper.Map<RecommendInfoModel>(item);
                await responseStream.WriteAsync(mappedResponse);
            });
        }
        public override async Task GetRecommendsListByToken(GetRecommendsListByTokenModel request, 
            IServerStreamWriter<RecommendInfoModel> responseStream, ServerCallContext context)
        {
            var mappedRequest = new GetRecommendsListModel() { ProfileId = this.ProfileId };
            await this.GetRecommendsList(mappedRequest, responseStream, context);
        }
    }
}
