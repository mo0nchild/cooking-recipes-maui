namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models
{
    public partial class CookingRecipesList : object
    {
        public required List<CookingRecipeInfo> Recipes { get; set; } = new();
        public required int AllCount { get; set; } = default!;
    }
}
