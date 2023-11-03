using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Authorization;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile
{
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class AuthorizationRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AuthorizationRequest, int?>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<int?> Handle(AuthorizationRequest request, CancellationToken cancellationToken)
        {
            var verifyPassword = (string hashPassword) =>
            {
                try { return BCryptType.Verify(request.Password, hashPassword, false, BCrypt.Net.HashType.SHA384); }
                catch (BCrypt.Net.SaltParseException) { return false; }
            };
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var profiles = await dbcontext.Authorizations.Where(item => item.Login == request.Login).ToListAsync();
                var result = profiles.FirstOrDefault(item => item.Login == request.Login && verifyPassword(item.Password));

                return result?.UserProfile.Id;
            }
        }
    }
}
