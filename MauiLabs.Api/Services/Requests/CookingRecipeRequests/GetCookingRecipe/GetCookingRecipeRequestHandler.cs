using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe
{
    public partial class GetCookingRecipeRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory, IMapper mapper)
        : IRequestHandler<GetCookingRecipeRequest, CookingRecipeInfo?>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CookingRecipeInfo?> Handle(GetCookingRecipeRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.CookingRecipes.Where(item => item.Id == request.RecipeId)
                    .Include(item => item.Publisher)
                    .Include(item => item.Comments)
                    .Include(item => item.Ingredients).FirstOrDefaultAsync();
                return this._mapper.Map<CookingRecipeInfo>(requestResult);
            }
        }
    }
}
