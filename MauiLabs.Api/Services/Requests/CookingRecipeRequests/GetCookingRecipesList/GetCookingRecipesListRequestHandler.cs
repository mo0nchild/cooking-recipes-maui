using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList
{
    public partial class GetCookingRecipesListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetCookingRecipesListRequest, CookingRecipesList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CookingRecipesList> Handle(GetCookingRecipesListRequest request, CancellationToken cancellationToken)
        {
            var sortedRating = (CookingRecipe item) => item.Comments.Sum(op => (double)op.Rating / item.Comments.Count());
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.CookingRecipes.Include(item => item.RecipeCategory)
                    .Where(item => request.Confirmed == null ? true : item.Confirmed == request.Confirmed)
                    .Where(item => request.Category == null ? true : item.RecipeCategory!.Name == request.Category)
                    .Where(item => request.TextFilter == null ? true : Regex.IsMatch(item.Name, request.TextFilter))
                    .Include(item => item.Publisher)
                    .Include(item => item.Comments)
                    .Include(item => item.Ingredients)
                    .Skip(request.Skip).Take(request.Take).ToListAsync();
                var orderedResult = (request.SortingType switch
                {
                    RecipeSortingType.ByRating => requestResult.OrderByDescending(item => sortedRating(item)),
                    RecipeSortingType.ByDate => requestResult.OrderByDescending(item => item.PublicationTime),
                    RecipeSortingType.ByName => requestResult.OrderBy(item => item.Name),
                    _ => throw new ApiServiceException("Не установлен режим сортировки", typeof(GetCookingRecipesListRequest))
                })
                .ToImmutableList();
                return new CookingRecipesList()
                {
                    AllCount = await dbcontext.CookingRecipes.CountAsync(),
                    Recipes = this._mapper.Map<List<CookingRecipeInfo>>(orderedResult),
                };
            }
        }
    }
}
