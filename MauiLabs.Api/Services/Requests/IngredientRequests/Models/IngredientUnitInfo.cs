using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.IngredientRequests.Models
{
    public partial class IngredientUnitInfo : IMappingTarget<IngredientUnit>
    {
        public required string Name { get; set; } = default!;
    }
}
