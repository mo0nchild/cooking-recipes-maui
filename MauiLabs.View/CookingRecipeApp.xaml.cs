using MauiLabs.View.Pages.ProfilePages;
using MauiLabs.View.Pages.RecipePages;

namespace MauiLabs.View
{
    public partial class CookingRecipeApp : Application
    {
        public CookingRecipeApp(IServiceProvider serviceProvider) : base()
        {
            this.InitializeComponent();
            this.MainPage = new CookingRecipeShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var applicationWindow = base.CreateWindow(activationState);
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                (applicationWindow.MaximumWidth, applicationWindow.MaximumHeight) = (500, 750);
                (applicationWindow.Width, applicationWindow.Height) = (500, 750);
            }
            return applicationWindow;
        }
    }
}