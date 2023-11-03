using AutoMapper;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo
{
    public partial class GetProfileInfoRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetProfileInfoRequest, ProfileInfo?>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<ProfileInfo?> Handle(GetProfileInfoRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.Id);
                return this._mapper.Map<ProfileInfo?>(profile);
            }
        }
    }
}
