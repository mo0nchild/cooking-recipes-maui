using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    using Parameters = IDictionary<string, object>;
    public partial class NavigationService : INavigationService
    {
        protected internal readonly IServiceProvider serviceProvider = default!;
        public NavigationService(IServiceProvider serviceProvider) : base() 
        {
            this.serviceProvider = serviceProvider;
        }
        protected virtual TPage GeneratePage<TPage>() => ActivatorUtilities.CreateInstance<TPage>(this.serviceProvider);
        public virtual async Task NavigateToPage<TPage>(Shell root, Parameters parameters = null) where TPage : Page
        {
            var pageInstance = this.serviceProvider.GetService<TPage>() ?? this.GeneratePage<TPage>();
            if (pageInstance is INavigationService.IQueryableNavigation queryAttributable && parameters != null)
            {
                queryAttributable.SetNavigationQuery(parameters);
            }
            pageInstance.Loaded += (_, _) => CookingRecipeShell.SetTabBarVisibility(pageInstance, false);

            await root.Navigation.PushAsync(pageInstance);
            //pageInstance.Unloaded += (_, _) => CookingRecipeShell.SetTabBarVisibility(pageInstance, true);
        }
    }
}
