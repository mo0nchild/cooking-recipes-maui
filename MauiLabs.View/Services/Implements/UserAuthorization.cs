using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;

namespace MauiLabs.View.Services.Implements
{
    using WebApiOptions = ConfigureWebApi.WebApiOptions;
    public partial class UserAuthorization : IUserAuthorization
    {
        protected internal readonly IHttpClientFactory httpClientFactory = default!;
        protected internal readonly WebApiOptions webApiOptions = default!;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public UserAuthorization(IHttpClientFactory httpFactory, IOptions<WebApiOptions> options) : base() 
        {
            this.httpClientFactory = httpFactory;
            this.webApiOptions = options.Value;
        }
        public virtual async Task AuthorizeUser(string login, string password)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var response = await httpClient.GetAsync($"cookingrecipes/auth/login?Login={login}&Password={password}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new Exception(errorMessage.Detail);
                }
                var resultMessage = await response.Content.ReadFromJsonAsync<LoginResponseModel>(jsonOptions);
                await SecureStorage.Default.SetAsync("JwtToken", resultMessage.JwtToken);

                await SecureStorage.Default.SetAsync("UserId", resultMessage.ProfileId.ToString());
                await SecureStorage.Default.SetAsync("IsAdmin", resultMessage.IsAdmin.ToString());
            }
        }

        public virtual Task RegistrationUser(RegistrationRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
