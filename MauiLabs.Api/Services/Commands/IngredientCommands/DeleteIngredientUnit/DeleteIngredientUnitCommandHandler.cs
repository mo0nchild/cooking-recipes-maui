using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientUnit
{
    public partial class DeleteIngredientUnitCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteIngredientUnitCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteIngredientUnitCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteIngredientUnitCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.IngredientUnits.Where(item => item.Name == request.Name).ExecuteDeleteAsync();
                if (result < 0) throw new ApiServiceException("Единица измерения не найдена", requestType);
            }
        }
    }
}
