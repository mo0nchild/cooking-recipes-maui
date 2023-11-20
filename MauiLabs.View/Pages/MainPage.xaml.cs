using MauiLabs.View.Services.Interfaces;

namespace MauiLabs.View.Pages
{
    public partial class MainPage : ContentPage
    {
        protected readonly IUserAuthorization authorization = default!;

        public MainPage(IUserAuthorization authorization) : base()
        {
            this.InitializeComponent();
            this.authorization = authorization;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("./userlist");
            this.activity.IsRunning = true;
            try
            {
                await this.authorization.AuthorizeUser("admin1", "1234567890");
                await this.DisplayAlert("Jwt Token", await SecureStorage.Default.GetAsync("JwtToken"), "Назад");
            }
            catch (Exception ex) 
            {
                await this.DisplayAlert("Jwt Token", ex.Message, "Назад");
            }

            this.IsBusy = false;
        }
    }
}