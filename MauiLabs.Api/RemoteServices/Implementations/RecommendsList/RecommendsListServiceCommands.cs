using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class RecommendsListServiceCommands(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : RecommendsListCommands.RecommendsListCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }
        public bool ProfileIsAdmin { get => this._contextAccessor.HttpContext!.User.IsInRole("Admin")!; }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public override async Task<Empty> AddRecommend(AddRecommendModel request, ServerCallContext context)
        {
            try { await this._mediator.Send(this._mapper.Map<AddRecommendCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        public override async Task<Empty> AddRecommendByToken(AddRecommendByTokenModel request, ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<AddRecommendCommand>(request);
            mappedRequest.FromUserId = this.ProfileId;

            try { await this._mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo) 
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        public override async Task<Empty> DeleteRecommend(DeleteRecommendModel request, ServerCallContext context)
        {
            var userFilter = (RecommendInfo info) => info.Id == request.RecordId;
            try {
                var recommends = await this._mediator.Send(new GetRecommendsListRequest() { ProfileId = this.ProfileId });
                if (!this.ProfileIsAdmin && recommends.Recommends.FirstOrDefault(p => userFilter.Invoke(p)) == null)
                {
                    throw new ValidationException("Рекомендация не принадлежит пользователю");
                }
                await this._mediator.Send(this._mapper.Map<DeleteRecommendCommand>(request)); 
            }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }
    }
}
