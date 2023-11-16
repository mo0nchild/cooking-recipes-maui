using AutoMapper;
using MauiLabs.Api.Services.Requests.IngredientRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.IngredientRequests.GetIngredientUnits
{
    public partial class GetIngredientUnitsRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetIngredientUnitsRequest, IngredientUnitsCollection>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<IngredientUnitsCollection> Handle(GetIngredientUnitsRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.IngredientUnits.ToListAsync();
                return new IngredientUnitsCollection()
                {
                    AllCount = result.Count,
                    IngredientUnits = this._mapper.Map<List<IngredientUnitInfo>>(result),
                };
            }
        }
    }
}
