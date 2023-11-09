using Grpc.Core;

namespace MauiLabs.Api.RemoteServices.Implementations
{
    public class CookingRecipeUserService : CookingRecipeUser.CookingRecipeUserBase
    {
        public CookingRecipeUserService() { }
        public override Task<AddCookingRecipeResponseModel> AddCookingRecipe(AddCookingRecipeRequestModel request, ServerCallContext context)
        {
            base.AddCookingRecipe(request, context);
            return Task.FromResult(new AddCookingRecipeResponseModel()
            {
                Error = ""
            });
        }
    }
}
