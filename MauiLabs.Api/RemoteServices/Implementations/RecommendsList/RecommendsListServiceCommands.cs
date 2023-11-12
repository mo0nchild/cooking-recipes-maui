using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    public partial class RecommendsListServiceCommands(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : RecommendsListCommands.RecommendsListCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public override async Task<Empty> AddRecommend(AddRecommendModel request, ServerCallContext context)
        {
            foreach(var item in context.GetHttpContext().User.Claims)
            {
                Console.WriteLine($"{item.Type} {item.Value}");
            }

            return new Empty();
        }

        public override Task<Empty> AddRecommendByToken(AddRecommendByTokenModel request, ServerCallContext context)
        {
            return base.AddRecommendByToken(request, context);
        }

        public override Task<Empty> DeleteRecommend(DeleteRecommendModel request, ServerCallContext context)
        {
            return base.DeleteRecommend(request, context);
        }
    }
}
