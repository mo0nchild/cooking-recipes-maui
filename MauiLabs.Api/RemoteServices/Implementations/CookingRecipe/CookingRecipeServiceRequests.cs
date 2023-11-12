using AutoMapper;
using Grpc.Core;
using MediatR;

namespace MauiLabs.Api.RemoteServices.Implementations.CookingRecipe
{
    public partial class CookingRecipeServiceRequests(IMediator mediator, IMapper mapper)
        : CookingRecipeRequests.CookingRecipeRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        public override Task<CookingRecipeInfoModel> GetCookingRecipe(GetCookingRecipeModel request, ServerCallContext context)
        {
            return base.GetCookingRecipe(request, context);
        }

        public override Task<CookingRecipeListModel> GetCookingRecipesList(GetCookingRecipesListModel request, ServerCallContext context)
        {
            return base.GetCookingRecipesList(request, context);
        }

        public override Task<CookingRecipeListModel> GetPublishedRecipeList(GetPublishedRecipeListModel request, ServerCallContext context)
        {
            return base.GetPublishedRecipeList(request, context);
        }

        public override Task<CookingRecipeListModel> GetPublisherRecipeListByToken(GetPublisherRecipeListByTokenModel request, 
            ServerCallContext context)
        {
            return base.GetPublisherRecipeListByToken(request, context);
        }
    }
}
