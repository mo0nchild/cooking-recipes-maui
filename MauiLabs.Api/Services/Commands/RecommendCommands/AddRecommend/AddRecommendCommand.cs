using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend
{
    public partial class AddRecommendCommand : IRequest, IMappingTarget<Recommendation>
    {
        public string Text { get; set; } = default!;
        public int RecipeId { get; set; } = default!;

        public int FromUserId { get; set; } = default!;
        public int ToUserId { get; set; } = default!;
    }
}
