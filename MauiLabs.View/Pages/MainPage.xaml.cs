namespace MauiLabs.View.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage() : base()
        {
            this.InitializeComponent();
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("./userlist");
        }
    }
}