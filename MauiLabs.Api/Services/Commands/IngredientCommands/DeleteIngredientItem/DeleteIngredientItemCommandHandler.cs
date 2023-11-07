using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientItem
{
    public partial class DeleteIngredientItemCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteIngredientItemCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteIngredientItemCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteIngredientItemCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.IngredientItems.Where(item => item.Name == request.Name).ExecuteDeleteAsync();
                if (result < 0) throw new ApiServiceException("Ингредиент не найдена", requestType);
            }
        }
    }
}
