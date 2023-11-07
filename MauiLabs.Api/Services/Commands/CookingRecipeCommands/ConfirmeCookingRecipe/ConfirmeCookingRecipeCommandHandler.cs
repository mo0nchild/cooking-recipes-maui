using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe
{
    public partial class ConfirmeCookingRecipeCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<ConfirmeCookingRecipeCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(ConfirmeCookingRecipeCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(ConfirmeCookingRecipeCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var cookingRecipe = await dbcontext.CookingRecipes.FirstOrDefaultAsync(item => item.Id == request.Id);
                if (cookingRecipe == null) throw new ApiServiceException("Рецепт не найден", requestType);

                cookingRecipe.Confirmed = request.Status;
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
