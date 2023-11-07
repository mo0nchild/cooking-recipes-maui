using MauiLabs.Api.Services.Requests.RecipeCategoryRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.RecipeCategoryRequests.GetRecipeCategoryList
{
    public partial class GetCategoryListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<GetCategoryListRequest, CategoryInfoList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task<CategoryInfoList> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.RecipeCategories.ToListAsync(cancellationToken);
                return new CategoryInfoList() { Categories = result.Select(item => item.Name).ToList() };
            }
        }
    }
}
