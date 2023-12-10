using MauiLabs.View.Pages;
using MauiLabs.View.Pages.ProfilePages;
using MauiLabs.View.Pages.RecipePages;
using System.Net;
using System.Runtime.CompilerServices;

namespace MauiLabs.View
{
    public partial class CookingRecipeShell : Shell
    {
        public static readonly BindableProperty MyTabIsEnabledProperty = BindableProperty.Create(
            "MyTabIsEnabled", typeof(bool), 
            typeof(CookingRecipeShell), true, propertyChanged: TabIsEnabledChangedHandler);

        public bool MyTabIsEnabled 
        { 
            get => (bool)this.GetValue(MyTabIsEnabledProperty); set => this.SetValue(MyTabIsEnabledProperty, value); 
        }
        protected static async void TabIsEnabledChangedHandler(BindableObject @object, object oldValue, object newValue)
        {
            await Console.Out.WriteLineAsync($"TabIsEnabled Value: {newValue}");
        }
        public static void SetMyTabIsEnabled(bool enabled = true)
        {
            if (Shell.Current is CookingRecipeShell recipeShell)
            {
                recipeShell.Dispatcher.Dispatch(() => recipeShell.MyTabIsEnabled = enabled);
            }
        }
        public static void SetTabBarVisibility(Page page, bool value)
        {
            if (Shell.Current.CurrentItem.Items.Count > 1)
            {
                Shell.SetTabBarIsVisible(page, value);
                ShellSection currentSection = Shell.Current.CurrentItem.CurrentItem;
                Shell.Current.CurrentItem.CurrentItem = null;
                Shell.Current.CurrentItem.CurrentItem = currentSection;
            }
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