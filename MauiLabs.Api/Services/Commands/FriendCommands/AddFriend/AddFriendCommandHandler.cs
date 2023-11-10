using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.FriendCommands.AddFriend
{
    public partial class AddFriendCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<AddFriendCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task Handle(AddFriendCommand request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                if((await dbcontext.UserProfiles.FirstOrDefaultAsync(p => p.Id == request.RequesterId)) == null) 
                {
                    throw new ApiServiceException("Профиль пользователя не найден", typeof(AddFriendCommand));
                }
                var profile = await dbcontext.UserProfiles.FirstOrDefaultAsync(p => p.ReferenceLink == request.ReferenceLink);
                if (profile == null) throw new ApiServiceException("Профиль друга не найден", typeof(AddFriendCommand));

                await dbcontext.FriendsList.AddRangeAsync(new FriendList()
                {
                    RequesterId = request.RequesterId, AddresseeId = profile.Id,
                    DateTime = DateTime.UtcNow,
                });
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
