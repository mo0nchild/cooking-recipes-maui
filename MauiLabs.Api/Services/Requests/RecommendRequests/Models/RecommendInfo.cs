using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.RecommendRequests.Models
{
    public partial class RecommendInfo : IMappingTarget<Recommendation>
    {
        public required int Id { get; set; } = default!;
        public required string Text { get; set; } = default!;

        public required ProfileInfo FromUser { get; set; } = default!;
        public required CookingRecipeInfo Recipe { get; set; } = default!;
    }
}
