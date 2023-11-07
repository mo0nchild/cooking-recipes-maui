using MediatR;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe
{
    public partial class ConfirmeCookingRecipeCommand : IRequest
    {
        public required int Id { get; set; } = default!;
        public required bool Status { get; set; } = default!;
    }
}
