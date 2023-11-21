using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Maui.Controls.PlatformConfiguration;
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
        protected internal readonly IHttpClientFactory httpClientFactory = default!;
        protected internal readonly WebApiOptions webApiOptions = default!;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public UserProfile(IHttpClientFactory httpFactory, IOptions<WebApiOptions> options) : base()
        {
            this.httpClientFactory = httpFactory;
            this.webApiOptions = options.Value;
        }
        public virtual async Task<string> ChangeProfilePassword(string token, ChangePasswordRequestModel model)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var requestModel = new HttpRequestMessage(HttpMethod.Patch, $"cookingrecipes/profile/editbytoken/password")
                {
                    Headers = { { "Authorization", $"Bearer {token}" } },
                    Content = JsonContent.Create(model),
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadAsStringAsync();
            }
        }
        public virtual async Task<string> DeleteProfileInfo(string token)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var requestModel = new HttpRequestMessage(HttpMethod.Delete, $"cookingrecipes/profile/deletebytoken")
                {
                    Headers = { { "Authorization", $"Bearer {token}" } }
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadAsStringAsync();
            }
        }
        public virtual async Task<string> EditProfileInfo(string token, EditProfileRequestModel model)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var requestModel = new HttpRequestMessage(HttpMethod.Put, $"cookingrecipes/profile/editbytoken")
                {
                    Headers = { { "Authorization", $"Bearer {token}" } },
                    Content = JsonContent.Create(model),
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadAsStringAsync();
            }
        }

        public virtual async Task<GetProfileInfoResponseModel> GetProfileInfo(string token, int id)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var requestModel = new HttpRequestMessage(HttpMethod.Get, $"cookingrecipes/profile/get?Id={id}")
                {
                    Headers = { { "Authorization", $"Bearer {token}" } },
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadFromJsonAsync<GetProfileInfoResponseModel>(jsonOptions);
            }
        }
        public virtual async Task<GetProfileInfoResponseModel> GetProfileInfoByToken(string token)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var requestModel = new HttpRequestMessage(HttpMethod.Get, $"cookingrecipes/profile/getbytoken")
                {
                    Headers = { { "Authorization", $"Bearer {token}" } },
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadFromJsonAsync<GetProfileInfoResponseModel>(jsonOptions);
            }
        }
        public virtual async Task<GetProfilesListResponseModel> GetProfilesList(string token, GetProfilesListRequestModel model)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                var requestUrl = string.Format("cookingrecipes/profile/getlist?TextFilter={0}&Skip={1}&Take={2}",
                    model.TextFilter, model.Skip, model.Take);
                using var requestModel = new HttpRequestMessage(HttpMethod.Get, requestUrl)
                {
                    Headers = { { "Authorization", $"Bearer {token}" } },
                };
                using var response = await httpClient.SendAsync(requestModel);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage.Detail);
                }
                return await response.Content.ReadFromJsonAsync<GetProfilesListResponseModel>(jsonOptions);
            }
        }
    }
}
