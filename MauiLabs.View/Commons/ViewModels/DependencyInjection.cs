﻿using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
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
            collection.AddTransient<RegistrationViewModel>();
            collection.AddTransient<ProfileInfoViewModel>();
            collection.AddTransient<FriendsListViewModel>();

            collection.AddTransient<RecipesListViewModel>();
            collection.AddTransient<PublishedListViewModel>();

            return Task.FromResult(collection);
        }
    }
}
