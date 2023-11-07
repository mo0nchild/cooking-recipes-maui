using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MauiLabs.Dal.Entities;
using MauiLabs.Api.Services.Commons.Exceptions;

namespace MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory
{
    public partial class AddRecipeCategoryCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<AddRecipeCategoryCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(AddRecipeCategoryCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(AddRecipeCategoryCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.RecipeCategories.FirstOrDefaultAsync(item => item.Name == request.Name) != null)
                {
                    throw new ApiServiceException("Категория с таким названием уже создана", requestType);
                }
                await dbcontext.RecipeCategories.AddRangeAsync(new RecipeCategory() { Name = request.Name });
                await dbcontext.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
