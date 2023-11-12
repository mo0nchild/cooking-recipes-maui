using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;

namespace MauiLabs.Api.RemoteServices.Implementations.CookingRecipe
{
    public partial class CookingRecipeServiceCommands(IMediator mediator, IMapper mapper)
        : CookingRecipeCommands.CookingRecipeCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        public override Task<Empty> AddCookingRecipeByToken(AddCookingRecipeByTokenModel request, ServerCallContext context)
        {
            return base.AddCookingRecipeByToken(request, context);
        }
        public override Task<Empty> AddCookingRecipe(AddCookingRecipeModel request, ServerCallContext context)
        {
            
            return base.AddCookingRecipe(request, context);
        }

        public override Task<Empty> ConfirmeCookingRecipe(ConfirmeCookingRecipeModel request, ServerCallContext context)
        {
            return base.ConfirmeCookingRecipe(request, context);
        }
        public override Task<Empty> DeleteCookingRecipe(DeleteCookingRecipeModel request, ServerCallContext context)
        {
            return base.DeleteCookingRecipe(request, context);
        }
        public override Task<Empty> EditCookingRecipe(EditCookingRecipeModel request, ServerCallContext context)
        {
            return base.EditCookingRecipe(request, context);
        }
    }
}
