using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe
{
    public partial class DeleteCookingRecipeCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteCookingRecipeCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteCookingRecipeCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteCookingRecipeCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.CookingRecipes.Where(item => item.Id == request.Id).ExecuteDeleteAsync();
                if (result <= 0) throw new ApiServiceException("Рецепт не найден", requestType);
            }
        }
    }
}
