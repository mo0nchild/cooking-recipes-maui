using AutoMapper;
using Grpc.Core;
using MediatR;
using System.Net.Mail;

namespace MauiLabs.Api.RemoteServices.Implementations
{
    public partial class CookingRecipeUserService(IMediator mediator, IMapper mapper) : CookingRecipeUser.CookingRecipeUserBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;

        public override Task<AddCookingRecipeResponseModel> AddCookingRecipe(AddCookingRecipeRequestModel request, ServerCallContext context)
        {
            base.AddCookingRecipe(request, context);
            
            return Task.FromResult(new AddCookingRecipeResponseModel()
            {
                
            });
        }
    }
}
