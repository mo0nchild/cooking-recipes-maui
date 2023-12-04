using MauiLabs.View.Pages;
using MauiLabs.View.Pages.ProfilePages;
using MauiLabs.View.Pages.RecipePages;
using System.Net;

namespace MauiLabs.View
{
    public partial class CookingRecipeShell : Shell
    {
        public static readonly BindableProperty TabIsEnabledProperty = BindableProperty.Create(
            "TabIsEnabled", typeof(bool), 
            typeof(CookingRecipeShell), true, propertyChanged: TabIsEnabledChangedHandler);

        public bool TabIsEnabled 
        { 
            get => (bool)this.GetValue(TabIsEnabledProperty); set => this.SetValue(TabIsEnabledProperty, value); 
        }
        protected static async void TabIsEnabledChangedHandler(BindableObject @object, object oldValue, object newValue)
        {
            await Console.Out.WriteLineAsync($"TabIsEnabled Value: {newValue}");
        }
        public static void SetTabIsEnabled(bool enabled = true)
        {
            if (Shell.Current is CookingRecipeShell recipeShell) recipeShell.TabIsEnabled = enabled;
        }
        public CookingRecipeShell() : base()
        {
            this.InitializeComponent();

            var aboutLinkTap = new TapGestureRecognizer() { NumberOfTapsRequired = 1 };
            aboutLinkTap.Tapped += this.AboutLinkTapHandler;

            this.AboutLabel.GestureRecognizers.Add(aboutLinkTap);
        }
        protected virtual async void AboutLinkTapHandler(object sender, TappedEventArgs args)
        {
            var pageUri = new Uri(@"https://github.com/mo0nchild/cs-maui-labs");

            try { await Browser.Default.OpenAsync(pageUri, BrowserLaunchMode.SystemPreferred); }
            catch (Exception errorInfo)
            {
                await this.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
            }
        }
        protected override void OnAppearing() => base.OnAppearing();
        protected override void OnDisappearing() => base.OnDisappearing();
    }
}