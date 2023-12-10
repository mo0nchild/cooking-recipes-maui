using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Responses;
using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IFriendsList
    {
        public Task<GetFriendsListResponseModel> GetFriendsListById(RequestInfo<GetFriendsListRequestModel> requestModel);
        public Task<GetFriendsListResponseModel> GetFriendsList(string token, CancellationToken cancelToken);

        public Task<string> AddFriend(RequestInfo<AddFriendRequestModel> requestModel);
        public Task<string> DeleteFriend(RequestInfo<DeleteFriendRequestModel> requestModel);
    }
}
