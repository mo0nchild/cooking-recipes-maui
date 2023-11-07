using AutoMapper;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList
{
    public partial class GetBookmarksListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetBookmarksListRequest, CookingRecipesList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<CookingRecipesList> Handle(GetBookmarksListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = dbcontext.Bookmarks.Where(item => item.ProfileId == request.ProfileId)
                    .Include(item => item.Recipe).ThenInclude(item => item.Publisher)
                    .Include(item => item.Recipe).ThenInclude(item => item.Comments)
                    .Include(item => item.Recipe).ThenInclude(item => item.Ingredients)
                    .Where(item => request.TextFilter == null ? true : Regex.IsMatch(item.Recipe.Name, request.TextFilter));
                var sortedResult = await (request.ReverseOrder switch
                {
                    false => requestResult.OrderByDescending(item => item.AddTime).ToListAsync(),
                    true => requestResult.OrderBy(item => item.AddTime).ToListAsync(),
                });
                return new CookingRecipesList() 
                {
                    AllCount = sortedResult.Count,
                    Recipes = this._mapper.Map<List<CookingRecipeInfo>>(sortedResult),
                };
            }
        }
    }
}
