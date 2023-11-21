using MauiLabs.View.ViewModels.ProfilesViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.ViewModels
{
    public static class DependencyInjection : object
    {
        public static Task<IServiceCollection> AddViewModels(this IServiceCollection collection, IConfiguration configuration)
        {
            collection.AddTransient<AuthorizationViewModel>();

            return Task.FromResult(collection);
        }
    }
}
