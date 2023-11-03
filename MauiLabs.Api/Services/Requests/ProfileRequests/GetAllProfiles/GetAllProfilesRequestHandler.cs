using AutoMapper;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles
{
    public partial class GetAllProfilesRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetAllProfilesRequest, ProfileCollection>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<ProfileCollection> Handle(GetAllProfilesRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var profiles = await dbcontext.UserProfiles.Skip(request.Skip).Take(request.Take).ToListAsync();
                return new ProfileCollection() 
                { 
                    AllCount = await dbcontext.UserProfiles.CountAsync(),
                    Profiles = this._mapper.Map<List<ProfileInfo>>(profiles) 
                };
            }
        }
    }
}
