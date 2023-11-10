using MauiLabs.Api.Commons.Mapping;
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
        public Dictionary<string, (double Value, string Unit)> Ingredients { get; set; } = new();
    }
}
