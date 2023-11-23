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
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Services.Implements
{
#nullable enable
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
        public virtual async Task<LoginResponseModel?> AuthorizeUser(LoginRequestModel model, CancellationToken token)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                var requestPath = string.Format("cookingrecipes/auth/login?Login={0}&Password={1}", model.Login, model.Password);
                using var response = await httpClient.GetAsync(requestPath, token);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage?.Detail ?? errorMessage?.Title);
                }
                return await response.Content.ReadFromJsonAsync<LoginResponseModel>(jsonOptions);
            }
        }

        public virtual async Task<LoginResponseModel?> RegistrationUser(RegistrationRequestModel model, CancellationToken token)
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var response = await httpClient.PostAsJsonAsync($"cookingrecipes/auth/registration", model, token);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (token.IsCancellationRequested) return null;
                    var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                    throw new ViewServiceException(errorMessage?.Detail ?? errorMessage?.Title);
                }
                return await response.Content.ReadFromJsonAsync<LoginResponseModel>(jsonOptions);
            }
        }
    }
#nullable disable
}
