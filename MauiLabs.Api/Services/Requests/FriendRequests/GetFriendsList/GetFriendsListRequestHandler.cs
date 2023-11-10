using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList
{
    public partial class GetFriendsListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<GetFriendsListRequest, FriendInfo>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;

        public async Task<FriendInfo> Handle(GetFriendsListRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                
            }
        }
    }
}
