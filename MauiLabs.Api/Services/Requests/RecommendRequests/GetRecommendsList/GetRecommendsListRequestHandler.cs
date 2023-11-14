using AutoMapper;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList
{
    public partial class GetRecommendsListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetRecommendsListRequest, RecommendInfoList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<RecommendInfoList> Handle(GetRecommendsListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var requestResult = await dbcontext.Recommendations
                    .Where(item => item.ToUserId == request.ProfileId || item.FromUserId == request.ProfileId)
                    .Include(item => item.FromUser).Include(item => item.ToUser)
                    .Include(item => item.Recipe).ToListAsync();
                return new RecommendInfoList()
                {
                    AllCount = requestResult.Count,
                    Recommends = this._mapper.Map<List<RecommendInfo>>(requestResult),
                };
            }
        }
    }
}
