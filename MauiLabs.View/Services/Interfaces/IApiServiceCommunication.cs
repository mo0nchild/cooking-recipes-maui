using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IApiServiceCommunication : IDisposable
    {
        public Task<TResponse> SendRequestAsync<TResponse>(HttpRequestMessage requestMessage, CancellationToken cancelToken, 
            Func<HttpContent, Task<TResponse>> contentTransform);
        public Task<TResponse> GetDataFromServer<TRequest, TResponse>(string requestPath,
            RequestInfo<TRequest> model) where TRequest : class;

        public Task<string> AddDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class;
        public Task<string> DeleteDataFromServer<TRequest>(string requestPath, RequestInfo<TRequest> model) 
            where TRequest : class;
        public Task<string> UpdateDataToServer<TRequest>(string requestPath, RequestInfo<TRequest> model,
            bool fullUpdate = true) where TRequest : class;
    }
}
