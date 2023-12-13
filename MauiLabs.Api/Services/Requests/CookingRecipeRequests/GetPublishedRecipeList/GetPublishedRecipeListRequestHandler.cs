using AutoMapper;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
                    .Include(item => item.RecipeCategory)
                    .Include(item => item.Ingredients)
                    .Where(item => request.TextFilter == null
                        ? true : Regex.IsMatch(item.Name, request.TextFilter, RegexOptions.IgnoreCase))
                    .Where(item => request.Category == null
                        ? true : request.Category == item.RecipeCategory.Name).ToListAsync();
                return new CookingRecipesList()
                {
                    AllCount = requestResult.Count,
                    Recipes = this._mapper.Map<List<CookingRecipeInfo>>(requestResult),
                };
            }
        }
    }
}
