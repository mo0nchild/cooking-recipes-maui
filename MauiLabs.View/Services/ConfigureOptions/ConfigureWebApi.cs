using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.ConfigureOptions
{
    public partial class ConfigureWebApi : IConfigureNamedOptions<ConfigureWebApi.WebApiOptions>
    {
        protected internal readonly IConfiguration configuration = default!;
        public ConfigureWebApi(IConfiguration configuration): base() => this.configuration = configuration;

        public sealed class WebApiOptions : object
        {
            public required string BaseUrl { get; set; } = default!;
            public required string ApiKey { get; set; } = default!;
            public required string ApiClient { get; set; } = default!;
        }
        public virtual void Configure(string name, WebApiOptions options) => this.Configure(options);

        public virtual void Configure(WebApiOptions options)
        {
            var webApiOptions = this.configuration.GetSection("WebApi").Get<WebApiOptions>();
            options.ApiClient = webApiOptions.ApiClient;

            (options.ApiKey, options.BaseUrl) = (webApiOptions.ApiKey, webApiOptions.BaseUrl);
        }
    }
}
