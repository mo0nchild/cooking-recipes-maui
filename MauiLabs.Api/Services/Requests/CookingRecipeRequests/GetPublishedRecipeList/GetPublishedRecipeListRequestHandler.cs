using AutoMapper;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList
{
    public partial class GetPublishedRecipeListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory, 
        IMapper mapper) : IRequestHandler<GetPublishedRecipeListRequest, CookingRecipesList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CookingRecipesList> Handle(GetPublishedRecipeListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.CookingRecipes.Where(item => item.PublisherId == request.PublisherId)
                    .Include(item => item.Publisher)
                    .Include(item => item.Comments)
                    .Include(item => item.Ingredients).ToListAsync();
                return new CookingRecipesList()
                {
                    AllCount = await dbcontext.CookingRecipes.CountAsync(),
                    Recipes = this._mapper.Map<List<CookingRecipeInfo>>(requestResult),
                };
            }
        }
    }
}
