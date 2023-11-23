using MauiLabs.View.Pages.ProfilePages;

namespace MauiLabs.View
{
    public partial class CookingRecipeApp : Application
    {
        public CookingRecipeApp() : base()
        {
            this.InitializeComponent();
            this.MainPage = new ProfileShell();
        }
        protected override Window CreateWindow(IActivationState activationState)
        {
            var applicationWindow = base.CreateWindow(activationState);
            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                (applicationWindow.MaximumWidth, applicationWindow.MaximumHeight) = (500, 700);
                (applicationWindow.Width, applicationWindow.Height) = (500, 700);
            }
            return applicationWindow;
        }
    }
}