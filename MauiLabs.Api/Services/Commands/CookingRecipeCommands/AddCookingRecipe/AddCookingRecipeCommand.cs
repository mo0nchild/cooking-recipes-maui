using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe
{
    public partial class AddCookingRecipeCommand : IRequest, IMappingTarget<CookingRecipe>
    {
        public required string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public byte[]? Image { get; set; } = default!;
        public string? Category { get; set; } = default!;

        public required int PublisherId { get; set; } = default!;
        public Dictionary<string, (double Value, string Unit)> Ingredients { get; set; } = new();
    }
}
