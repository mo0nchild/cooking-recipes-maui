using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    using WebApiOptions = ConfigureWebApi.WebApiOptions;
    public partial class UserProfile : IUserProfile
    {
        protected internal readonly IApiServiceCommunication apiService = default!;
        public UserProfile(IApiServiceCommunication apiService) : base() => this.apiService = apiService;

        public virtual async Task<string> ChangeProfilePassword(RequestInfo<ChangePasswordRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/profile/editbytoken/password");
            return await this.apiService.UpdateDataToServer<ChangePasswordRequestModel>(requestPath, requestModel, false);
        }
        public virtual async Task<string> DeleteProfileInfo(string token, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/profile/deletebytoken");
            using var request = new HttpRequestMessage(HttpMethod.Delete, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync<string>(request, cancelToken, async msg => await msg.ReadAsStringAsync());
            
        }
        public virtual async Task<string> EditProfileInfo(RequestInfo<EditProfileRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/profile/editbytoken");
            return await this.apiService.UpdateDataToServer<EditProfileRequestModel>(requestPath, requestModel);
        }

        public virtual async Task<GetProfileInfoResponseModel> GetProfileInfo(string token, int id, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/profile/get?Id={0}", id);
            using var request = new HttpRequestMessage(HttpMethod.Get, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync(request, cancelToken, async content =>
            {
                return JsonConvert.DeserializeObject<GetProfileInfoResponseModel>(await content.ReadAsStringAsync());
            });
        }
        public virtual async Task<GetProfileInfoResponseModel> GetProfileInfoByToken(string token, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/profile/getbytoken");
            using var request = new HttpRequestMessage(HttpMethod.Get, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync(request, cancelToken, async content =>
            {
                return JsonConvert.DeserializeObject<GetProfileInfoResponseModel>(await content.ReadAsStringAsync());
            });
        }
        public virtual async Task<GetProfilesListResponseModel> GetProfilesList(RequestInfo<GetProfilesListRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/profile/getlist");
            return await this.apiService
                .GetDataFromServer<GetProfilesListRequestModel, GetProfilesListResponseModel>(requestPath, requestModel);
        }
    }
}
