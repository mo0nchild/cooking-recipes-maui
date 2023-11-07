using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList
{
    public partial class GetCookingRecipesListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetCookingRecipesListRequest, CookingRecipesList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CookingRecipesList> Handle(GetCookingRecipesListRequest request, CancellationToken cancellationToken)
        {
            var filterName = (string name) => request.TextFilter == null ? true : Regex.IsMatch(name, request.TextFilter);
            var filterCategory = (RecipeCategory? category) => category?.Name == request.Category;

            var sortedRating = (CookingRecipe item) => item.Comments.Sum(op => (double)op.Rating / item.Comments.Count());
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = dbcontext.CookingRecipes.Include(item => item.RecipeCategory)
                    .Where(item => filterName(item.Name) && item.Confirmed == request.Confirmed && filterCategory(item.RecipeCategory))
                    .Include(item => item.Publisher)
                    .Include(item => item.Comments)
                    .Include(item => item.Ingredients);
                var orderedResult = await (request.SortingType switch
                {
                    RecipeSortingType.ByRating => requestResult.OrderByDescending(item => sortedRating(item)),
                    RecipeSortingType.ByDate => requestResult.OrderByDescending(item => item.PublicationTime),
                    RecipeSortingType.ByName => requestResult.OrderBy(item => item.Name),
                    _ => throw new ApiServiceException("Не установлен режим сортировки", typeof(GetCookingRecipesListRequest))
                })
                .Skip(request.Skip).Take(request.Take).ToListAsync();
                return new CookingRecipesList()
                {
                    AllCount = await dbcontext.CookingRecipes.CountAsync(),
                    Recipes = this._mapper.Map<List<CookingRecipeInfo>>(orderedResult),
                };
            }
        }
    }
}
