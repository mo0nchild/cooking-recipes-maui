using AutoMapper;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList
{
    public partial class GetFriendsListRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetFriendsListRequest, FriendInfoList>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<FriendInfoList> Handle(GetFriendsListRequest request, CancellationToken cancellationToken)
        {
            var findFilter = (FriendList item) => item.RequesterId == request.ProfileId || item.AddresseeId == request.ProfileId;
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var friendRecord = await dbcontext.FriendsList.Where(item => findFilter.Invoke(item))
                    .Include(item => item.Requester)
                    .Include(item => item.Addressee).ToListAsync();
                return new FriendInfoList()
                {
                    Friends = friendRecord.Select(el => new FriendInfo() 
                    {
                        DateTime = el.DateTime, Id = el.Id,
                        Profile = this._mapper.Map<ProfileInfo>(el.AddresseeId == request.ProfileId ? el.Requester : el.Addressee)
                    })
                    .ToList(), AllCount = friendRecord.Count,
                };
            }
        }
    }
}
