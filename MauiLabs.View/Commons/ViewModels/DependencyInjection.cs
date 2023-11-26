using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Commons.ViewModels.RecipesViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Commons.ViewModels
{
    public static class DependencyInjection : object
    {
        public static Task<IServiceCollection> AddViewModels(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddTransient<AuthorizationViewModel>();

            collection.AddTransient<RecipesListViewModel>();

            return Task.FromResult(collection);
        }
    }
}
