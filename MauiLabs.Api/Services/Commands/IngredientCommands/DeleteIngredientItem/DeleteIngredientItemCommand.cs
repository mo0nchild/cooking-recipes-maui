using MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientItem
{
    public partial class DeleteIngredientItemCommand : IRequest
    {
        public required string Name { get; set; } = default!;
    }
}
