using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    public partial class FriendsList : IFriendsList
    {
        protected internal readonly IApiServiceCommunication apiService = default!;
        public FriendsList(IApiServiceCommunication apiService) : base() => this.apiService = apiService;

        public virtual async Task<GetFriendsListResponseModel> GetFriendsListById(RequestInfo<GetFriendsListRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/friends/getlist");
            return await this.apiService.GetDataFromServer<GetFriendsListRequestModel, GetFriendsListResponseModel>(requestPath, model);
        }
        public virtual async Task<GetFriendsListResponseModel> GetFriendsList(string token, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/friends/getlistbytoken");
            using var request = new HttpRequestMessage(HttpMethod.Get, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync(request, cancelToken, async content =>
            {
                return JsonConvert.DeserializeObject<GetFriendsListResponseModel>(await content.ReadAsStringAsync());
            });
        }
        public virtual async Task<string> AddFriend(RequestInfo<AddFriendRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/friends/addbytoken");
            return await this.apiService.AddDataToServer<AddFriendRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> DeleteFriend(RequestInfo<DeleteFriendRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/friends/delete");
            return await this.apiService.DeleteDataFromServer<DeleteFriendRequestModel>(requestPath, requestModel);
        }
    }
}
