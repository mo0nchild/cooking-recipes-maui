using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class CookingRecipeInfo : IMappingTarget<CookingRecipe>
    {
        public required int Id { get; set; } = default!;
        public required string Name { get; set; } = default!;

        public string? Description { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public required DateTime PublicationTime { get; set; } = default!;
        public required double Rating { get; set; } = default!;
        public required bool Confirmed { get; set; } = default!;

        public required List<IngredientInfo> Ingredients { get; set; } = new();
        public required PublisherInfo Publisher { get; set; } = default!;
        public required int PublisherId { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            var averageFilter = (CookingRecipe p) => p.Comments.Sum(op => (double)op.Rating / p.Comments.Count());

            profile.CreateMap<CookingRecipe, CookingRecipeInfo>()
                .ForMember(item => item.Rating, options => options.MapFrom(p => averageFilter.Invoke(p)));
        }
    }
}
