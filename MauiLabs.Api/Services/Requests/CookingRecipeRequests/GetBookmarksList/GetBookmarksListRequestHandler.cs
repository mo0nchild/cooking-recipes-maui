using AutoMapper;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList
{
    public partial class GetBookmarksListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetBookmarksListRequest, BookmarksList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<BookmarksList> Handle(GetBookmarksListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Bookmarks.Where(item => item.ProfileId == request.ProfileId)
                    .Include(item => item.Recipe).ThenInclude(item => item.Publisher)
                    .Include(item => item.Recipe).ThenInclude(item => item.Comments)
                    .Include(item => item.Recipe).ThenInclude(item => item.RecipeCategory)
                    .Include(item => item.Recipe).ThenInclude(item => item.Ingredients)
                    .Where(item => request.TextFilter == null 
                        ? true : Regex.IsMatch(item.Recipe.Name, request.TextFilter, RegexOptions.IgnoreCase))
                    .Where(item => request.Category == null
                        ? true : request.Category == item.Recipe.RecipeCategory.Name)
                    .ToListAsync();
                var sortedResult = (request.ReverseOrder switch
                {
                    false => requestResult.OrderByDescending(item => item.AddTime),
                    true => requestResult.OrderBy(item => item.AddTime),
                })
                .ToImmutableList();
                return new BookmarksList() 
                {
                    Bookmarks = this._mapper.Map<List<BookmarkInfo>>(sortedResult),
                    AllCount = sortedResult.Count,
                };
            }
        }
    }
}
