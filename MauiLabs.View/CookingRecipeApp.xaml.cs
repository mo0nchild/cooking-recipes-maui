namespace MauiLabs.View
{
    public partial class CookingRecipeApp : Application
    {
        public CookingRecipeApp() : base()
        {
            this.InitializeComponent();

            MainPage = new AppShell();
        }
    }
}