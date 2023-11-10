using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientUnit
{
    public partial class AddIngredientUnitCommand : IRequest, IMappingTarget<IngredientUnit>
    {
        public required string Name { get; set; } = default!;
    }
}
