using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.CommentRequests.Models
{
    public partial class CommentInfo : IMappingTarget<Comment>
    {
        public required int Id { get; set; } = default!;
        public required string Text { get; set; } = string.Empty;
        public required double Rating { get; set; } = default!;
        public required DateTime PublicationTime { get; set; } = default;

        public required ProfileInfo Profile { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;
    }
}
