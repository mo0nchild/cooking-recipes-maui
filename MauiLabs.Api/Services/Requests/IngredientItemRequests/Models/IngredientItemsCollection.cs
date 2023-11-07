namespace MauiLabs.Api.Services.Requests.IngredientItemRequests.Models
{
    public partial class IngredientItemsCollection : object
    {
        public required List<IngredientItemInfo> IngredientItems { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
