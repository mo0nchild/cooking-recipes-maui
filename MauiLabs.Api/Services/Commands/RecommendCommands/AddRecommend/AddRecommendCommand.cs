using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend
{
    public partial class AddRecommendCommand : IRequest, IMappingTarget<Recommendation>
    {
        public required string Text { get; set; } = default!;
        public required int RecipeId { get; set; } = default!;

        public required int FromUserId { get; set; } = default!;
        public required int ToUserId { get; set; } = default!;
    }
}
