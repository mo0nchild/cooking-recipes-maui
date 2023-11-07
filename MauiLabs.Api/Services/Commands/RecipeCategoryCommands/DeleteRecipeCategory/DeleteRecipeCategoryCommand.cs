using MediatR;

namespace MauiLabs.Api.Services.Commands.RecipeCategoryCommands.DeleteRecipeCategory
{
    public partial class DeleteRecipeCategoryCommand : IRequest
    {
        public required string Name { get; set; } = default!;
    }
}
