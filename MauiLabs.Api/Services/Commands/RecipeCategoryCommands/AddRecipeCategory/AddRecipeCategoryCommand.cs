using MediatR;

namespace MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory
{
    public partial class AddRecipeCategoryCommand : IRequest
    {
        public required string Name { get; set; } = default!;
    }
}
