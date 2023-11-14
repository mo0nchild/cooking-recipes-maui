using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.Models;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe
{
    public partial class EditCookingRecipeCommand : IRequest, IMappingTarget<CookingRecipe>
    {
        public required int Id { get; set; } = default!;
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public byte[]? Image { get; set; } = default!;

        public required string? Category { get; set; } = default!;
        public Dictionary<string, IngredientUnitInfo> Ingredients { get; set; } = new();

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<AddCookingRecipeCommand, CookingRecipe>()
                .ForMember(item => item.PublicationTime, options => options.MapFrom(p => DateTime.UtcNow))
                .ForMember(item => item.Ingredients, options => options.Ignore());
        }
    }
}
