using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.Api.Services.Requests.IngredientItemRequests.Models
{
    public partial class IngredientItemInfo : IMappingTarget<IngredientInfo>
    {
        public required string Name { get; set; } = default!;
        public required string Unit { get; set; } = default!;
    }
}
