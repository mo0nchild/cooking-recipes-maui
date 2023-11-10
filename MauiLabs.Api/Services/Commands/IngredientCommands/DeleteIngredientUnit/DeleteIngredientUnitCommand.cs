using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientUnit
{
    public partial class DeleteIngredientUnitCommand : IRequest
    {
        public required string Name { get; set; } = default!;
    }
}
