using MauiLabs.View.Pages.ProfilePages;
using MauiLabs.View.Pages.RecipePages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Pages
{
    public static class DependencyInjection : object
    {
        public static Task<IServiceCollection> AddViewPages(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddTransient<AuthorizationPage>();
            collection.AddTransient<RegistrationPage>();
            collection.AddTransient<ProfileInfoPage>();
            collection.AddTransient<FriendsListPage>();
            collection.AddTransient<FriendInfoPage>();

            collection.AddTransient<RecipesListPage>();

            return Task.FromResult(collection);
        } 
    }
}
