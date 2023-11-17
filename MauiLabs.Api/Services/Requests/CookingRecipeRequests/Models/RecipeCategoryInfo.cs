using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class RecipeCategoryInfo : IMappingTarget<RecipeCategory>
    {
        public required int Id { get; set; } = default;
        public required string Name { get; set; } = string.Empty;
    }
}
