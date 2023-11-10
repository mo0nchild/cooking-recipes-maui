using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class IngredientInfo : IMappingTarget<IngredientsList>
    {
        public required double Value { get; set; } = default!;
        public required string Name { get; set; } = default!;
        public required string Unit { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<IngredientsList, IngredientInfo>()
                .ForMember(item => item.Value, options => options.MapFrom(p => p.Value))
                .ForMember(item => item.Name, options => options.MapFrom(p => p.Name))
                .ForMember(item => item.Unit, options => options.MapFrom(p => p.IngredientUnit.Name));
        }
    }
}
