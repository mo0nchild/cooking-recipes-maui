using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem
{
    public partial class AddIngredientItemCommand : IRequest, IMappingTarget<IngredientItem>
    {
        public string IngredientName { get; set; } = default!;
        public string UnitName { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<AddIngredientItemCommand, IngredientItem>()
                .ForMember(p => p.Name, options => options.MapFrom(p => p.IngredientName))
                .ForMember(p => p.Unit, options => options.MapFrom(p => p.UnitName));
        }
    }
}
