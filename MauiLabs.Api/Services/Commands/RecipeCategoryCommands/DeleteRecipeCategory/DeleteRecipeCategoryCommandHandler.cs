using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.RecipeCategoryCommands.DeleteRecipeCategory
{
    public partial class DeleteRecipeCategoryCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<DeleteRecipeCategoryCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(DeleteRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteRecipeCategoryCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.RecipeCategories.Where(item => item.Name == request.Name).ExecuteDeleteAsync();
                if (result < 0) throw new ApiServiceException("Категория не найдена", requestType);
            }
        }
    }
}
