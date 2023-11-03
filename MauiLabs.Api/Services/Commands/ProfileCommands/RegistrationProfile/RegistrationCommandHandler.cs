using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile
{
    public partial class RegistrationCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<RegistrationCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var collisionDetection = await dbcontext.UserProfiles.Include(p => p.Authorization)
                    .FirstOrDefaultAsync(p => p.Authorization.Login == request.Login);
                if (collisionDetection != null)
                {
                    throw new ApiServiceException("Логин или почта уже используется", typeof(RegistrationCommand));
                }
                await dbcontext.UserProfiles.AddAsync(_mapper.Map<UserProfile>(request));
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
