using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe
{
    public partial class GetCookingRecipeRequest : IRequest<CookingRecipeInfo?>
    {
        public required int RecipeId { get; set; } = default!;
    }
}
