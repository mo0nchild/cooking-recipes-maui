using AutoMapper;
using MauiLabs.Api.Services.Requests.IngredientItemRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.IngredientItemRequests.GetIngredientItems
{
    public partial class GetIngredientItemsRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetIngredientItemsRequest, IngredientItemsCollection>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<IngredientItemsCollection> Handle(GetIngredientItemsRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.IngredientItems.ToListAsync();
                return new IngredientItemsCollection() 
                { 
                    AllCount = result.Count,
                    IngredientItems = this._mapper.Map<List<IngredientItemInfo>>(result),
                };
            }
        }
    }
}
