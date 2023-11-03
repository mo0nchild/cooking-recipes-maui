namespace MauiLabs.View
{
    public partial class App : Application
    {
        public App() : base()
        {
            this.InitializeComponent();

            MainPage = new AppShell();
        }
    }
}