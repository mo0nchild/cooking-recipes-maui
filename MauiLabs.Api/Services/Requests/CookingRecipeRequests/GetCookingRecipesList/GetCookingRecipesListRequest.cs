using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList
{
    public enum RecipeSortingType : sbyte { ByDate, ByName, ByRating }
    public partial class GetCookingRecipesListRequest : IRequest<CookingRecipesList>
    {
        public required int Skip { get; set; } = default!;
        public required int Take { get; set; } = default!;
        public bool? Confirmed { get; set; } = default!;

        public required RecipeSortingType SortingType { get; set; } = default!;
        public string? Category { get; set; } = default!;
        public string? TextFilter { get; set; } = default;
    }
}
