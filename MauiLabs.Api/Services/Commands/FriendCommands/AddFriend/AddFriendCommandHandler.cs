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
            var requestType = typeof(AddFriendCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(p => p.Id == request.RequesterId) == null) 
                {
                    throw new ApiServiceException("Профиль пользователя не найден", requestType);
                }
                var profile = await dbcontext.UserProfiles.FirstOrDefaultAsync(p => p.ReferenceLink == request.ReferenceLink);
                if (profile == null) throw new ApiServiceException("Профиль друга не найден", requestType);

                var collision = await dbcontext.FriendsList.Include(item => item.Addressee).Include(item => item.Requester)
                    .FirstOrDefaultAsync(item => 
                        (item.RequesterId == request.RequesterId && item.Addressee.ReferenceLink == request.ReferenceLink) ||
                        (item.AddresseeId == request.RequesterId && item.Requester.ReferenceLink == request.ReferenceLink));

                if (collision != null) throw new ApiServiceException("Пользователь уже в друзьях", requestType);
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
