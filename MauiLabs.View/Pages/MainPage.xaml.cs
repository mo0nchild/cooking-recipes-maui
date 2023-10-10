namespace MauiLabs.View.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly AnotherPage anotherPage;

        public MainPage(AnotherPage anotherPage) : base()
        {
            this.InitializeComponent();
            this.anotherPage = anotherPage;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(this.anotherPage, false);
        }
    }
}