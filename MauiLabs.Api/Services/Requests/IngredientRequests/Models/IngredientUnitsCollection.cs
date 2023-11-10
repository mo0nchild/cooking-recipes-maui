namespace MauiLabs.Api.Services.Requests.IngredientRequests.Models
{
    public partial class IngredientUnitsCollection : object
    {
        public required List<IngredientUnitInfo> IngredientUnits { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
