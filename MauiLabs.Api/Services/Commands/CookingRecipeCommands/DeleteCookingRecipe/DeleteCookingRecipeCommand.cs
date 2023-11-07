using MediatR;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe
{
    public partial class DeleteCookingRecipeCommand : IRequest
    {
        public required int Id { get; set; } = default!;
    }
}
