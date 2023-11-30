using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    using WebApiOptions = ConfigureWebApi.WebApiOptions;
    public partial class ApiServiceCommunication : IApiServiceCommunication
    {
        protected internal readonly IHttpClientFactory httpClientFactory = default!;
        protected internal readonly WebApiOptions webApiOptions = default!;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public ApiServiceCommunication(IHttpClientFactory httpFactory, IOptions<WebApiOptions> options) : base()
        {
            (this.httpClientFactory, this.webApiOptions) = (httpFactory, options.Value);
        }
        public virtual void Dispose() { }
        protected virtual async Task<TResponse> SendRequestAsync<TResponse>(HttpClient httpClient, HttpRequestMessage requestMessage, 
            CancellationToken cancelToken, Func<HttpContent, Task<TResponse>> contentTransform)
        {
            using var response = await httpClient.SendAsync(requestMessage, cancelToken);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadFromJsonAsync<ProblemDetails>(jsonOptions);
                throw new ViewServiceException(errorMessage?.Detail ?? errorMessage?.Title, response.StatusCode);
            }
            return await contentTransform.Invoke(response.Content);
        }

        public virtual async Task<string> AddDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var request = model.CreateRequestMessage(HttpMethod.Post, $"{httpClient.BaseAddress}{requestPath}");
                return await this.SendRequestAsync(httpClient, request, 
                    model.CancelToken,  async (content) => await content.ReadAsStringAsync());
            }
        }
        public virtual async Task<string> DeleteDataFromServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var request = model.CreateRequestMessage(HttpMethod.Delete, $"{httpClient.BaseAddress}{requestPath}");
                return await this.SendRequestAsync(httpClient, request,
                    model.CancelToken, async (content) => await content.ReadAsStringAsync());
            }
        }
        public virtual async Task<TResponse> GetDataFromServer<TRequest, TResponse>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var request = model.CreateRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}{requestPath}");
                return await this.SendRequestAsync(httpClient, request, model.CancelToken, async (content) => 
                {
                    var response = await content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(response);
                });
            }
        }
        public virtual async Task<string> UpdateDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model, 
            bool fullUpdate = true) where TRequest : class
        {
            var httpMethod = fullUpdate ? HttpMethod.Put : HttpMethod.Patch;
            using (var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient))
            {
                using var request = model.CreateRequestMessage(httpMethod, $"{httpClient.BaseAddress}{requestPath}");
                return await this.SendRequestAsync(httpClient, request,
                    model.CancelToken, async (content) => await content.ReadAsStringAsync());
            }
        }
    }
}
