using System.Net.Http.Json;
using System.Web;

namespace MauiLabs.View.Services.Commons
{
    public partial class RequestInfo<TRequest> : object where TRequest : class
    {
        public required CancellationToken CancelToken { get; set; } = default!;
        public required string ProfileToken { get; set; } = default!;
        public required TRequest RequestModel { get; set; } = default!;

        protected virtual string GetQueryString(TRequest model)
        {
            var properties = from item in model.GetType().GetProperties()
                             where item.GetValue(model, null) != null
                             select item.Name + "=" + HttpUtility.UrlEncode(item.GetValue(model, null).ToString());
            return String.Join("&", properties.ToArray());
        }

        public HttpRequestMessage CreateRequestMessage(HttpMethod method, string path)
        {
            var bodyUsing = !(method == HttpMethod.Delete || method == HttpMethod.Get);
            var urlBuilder = new UriBuilder(new Uri(path));

            if (!bodyUsing) urlBuilder.Query = this.GetQueryString(this.RequestModel);
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Uri)
            { 
                Headers = { { "Authorization", $"Bearer {this.ProfileToken}" } },
                Content = bodyUsing ? JsonContent.Create(this.RequestModel) : null,
            };
            return requestMessage;
        }
        public override int GetHashCode() => this.RequestModel.GetHashCode();
        public override string ToString() => base.ToString();
    }
}
