using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class BookmarkInfo : IMappingTarget<Bookmark>
    {
        public required int Id { get; set; } = default!;
        public required DateTime AddTime { get; set; } = default!;
        public required CookingRecipeInfo Recipe { get; set; } = default!;
    }
}
