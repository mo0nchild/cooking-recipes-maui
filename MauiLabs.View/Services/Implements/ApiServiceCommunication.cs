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

        public virtual Uri BaseAddress { get; protected set; } = default!;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        public ApiServiceCommunication(IHttpClientFactory httpFactory, IOptions<WebApiOptions> options) : base()
        {
            (this.httpClientFactory, this.webApiOptions) = (httpFactory, options.Value);

            using var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient);
            this.BaseAddress = httpClient.BaseAddress;
        }
        public virtual void Dispose() { }
        public virtual async Task<TResponse> SendRequestAsync<TResponse>(HttpRequestMessage requestMessage, 
            CancellationToken cancelToken, Func<HttpContent, Task<TResponse>> contentTransform)
        {
            using var httpClient = this.httpClientFactory.CreateClient(this.webApiOptions.ApiClient);            
            var responseTask = httpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, cancelToken);

            if (await responseTask.WaitUntil(cancelToken)) throw new TaskCanceledException();

            using var response = await responseTask;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode != HttpStatusCode.BadRequest) throw new Exception("Не удается подключиться к серверу");

                var responseContent = await response.Content.ReadAsStringAsync();
                var errorMessage = JsonConvert.DeserializeObject<ProblemDetails>(responseContent);

                throw new ViewServiceException(errorMessage?.Detail ?? errorMessage?.Title, response.StatusCode);
            }
            return await contentTransform.Invoke(response.Content);
        }

        public virtual async Task<string> AddDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using var request = model.CreateRequestMessage(HttpMethod.Post, $"{this.BaseAddress}{requestPath}");
            return await this.SendRequestAsync(request, model.CancelToken,  async (content) => await content.ReadAsStringAsync());
        }
        public virtual async Task<string> DeleteDataFromServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using var request = model.CreateRequestMessage(HttpMethod.Delete, $"{this.BaseAddress}{requestPath}");
            return await this.SendRequestAsync(request, model.CancelToken, async (content) => await content.ReadAsStringAsync());
        }
        public virtual async Task<TResponse> GetDataFromServer<TRequest, TResponse>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class
        {
            using var request = model.CreateRequestMessage(HttpMethod.Get, $"{this.BaseAddress}{requestPath}");
            return await this.SendRequestAsync(request, model.CancelToken, async (content) => 
            {
                return JsonConvert.DeserializeObject<TResponse>(await content.ReadAsStringAsync());
            });
        }
        public virtual async Task<string> UpdateDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model, 
            bool fullUpdate = true) where TRequest : class
        {
            using var request = model.CreateRequestMessage(fullUpdate ? HttpMethod.Put : HttpMethod.Patch, 
                $"{this.BaseAddress}{requestPath}");
            return await this.SendRequestAsync(request, model.CancelToken, async (content) => await content.ReadAsStringAsync());
        }
    }
    public static class TaskCancellationExtension : object
    {
        public static async Task<bool> WaitUntil(this Task thisTask, CancellationToken token)
        {
            while (!thisTask.IsCompleted && !token.IsCancellationRequested)
            {
                await Task.Delay(100);
            }
            return thisTask.Status != TaskStatus.RanToCompletion && token.IsCancellationRequested;
        }
    }
}
