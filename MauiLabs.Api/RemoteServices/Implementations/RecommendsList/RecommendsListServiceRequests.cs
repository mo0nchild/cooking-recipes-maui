using AutoMapper;
using Grpc.Core;
using MediatR;

namespace MauiLabs.Api.RemoteServices.Implementations.RecommendsList
{
    public partial class RecommendsListServiceRequests(IMediator mediator, IMapper mapper)
        : RecommendsListRequests.RecommendsListRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        public override Task<RecommendInfoListModel> GetRecommendsList(GetRecommendsListModel request, ServerCallContext context)
        {
            return base.GetRecommendsList(request, context);
        }

        public override Task<RecommendInfoListModel> GetRecommendsListByToken(GetRecommendsListByTokenModel request, ServerCallContext context)
        {
            return base.GetRecommendsListByToken(request, context);
        }
    }
}
