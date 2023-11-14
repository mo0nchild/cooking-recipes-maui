namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.Models
{
    public partial class IngredientUnitInfo : object
    {
        public required string Unit { get; set; } = default!;
        public required double Value { get; set; } = default!;
    }
}
