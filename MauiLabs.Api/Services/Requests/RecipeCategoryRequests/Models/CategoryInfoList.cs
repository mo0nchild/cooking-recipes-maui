namespace MauiLabs.Api.Services.Requests.RecipeCategoryRequests.Models
{
    public partial class CategoryInfoList : object
    {
        public required List<string> Categories { get; set; } = new();
    }
}
