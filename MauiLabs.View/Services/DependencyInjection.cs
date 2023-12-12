using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Implements;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services
{
    using WebApiOptions = ConfigureWebApi.WebApiOptions;
    public static class DependencyInjection : object
    {
        public static Task<IServiceCollection> AddViewServices(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.ConfigureOptions<ConfigureWebApi>();
            var clientOptions = collection.BuildServiceProvider().GetService<IOptions<WebApiOptions>>().Value;

            collection.AddHttpClient(clientOptions.ApiClient, options =>
            {
                options.DefaultRequestHeaders.Add("ApiKey", clientOptions.ApiKey);
                options.Timeout = TimeSpan.FromMilliseconds(20000);
                options.BaseAddress = new Uri(clientOptions.BaseUrl);
            });
            collection.AddTransient<IApiServiceCommunication, ApiServiceCommunication>();
            collection.AddTransient<INavigationService, NavigationService>();

            collection.AddTransient<IUserAuthorization, UserAuthorization>();
            collection.AddTransient<IUserProfile, UserProfile>();
            collection.AddTransient<IFriendsList, FriendsList>();

            collection.AddTransient<ICookingRecipes, CookingRecipes>();
            return Task.FromResult(collection);
        }
    }
}
