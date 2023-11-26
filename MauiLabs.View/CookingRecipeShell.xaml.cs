using MauiLabs.View.Pages;
using MauiLabs.View.Pages.ProfilePages;
using MauiLabs.View.Pages.RecipePages;
using System.Net;

namespace MauiLabs.View
{
    public partial class CookingRecipeShell : Shell
    {
        public CookingRecipeShell() : base()
        {
            this.InitializeComponent();
            //Routing.RegisterRoute(nameof(AuthorizationPage), typeof(AuthorizationPage));
            //Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));

            //Routing.RegisterRoute(nameof(RecipesListPage), typeof(RecipesListPage));
        }
    }
}