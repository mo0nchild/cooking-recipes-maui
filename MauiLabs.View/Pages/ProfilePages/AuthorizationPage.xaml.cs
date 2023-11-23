using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class AuthorizationPage : ContentPage
{
    protected internal bool isPageLoaded = default!;
    public AuthorizationPage(AuthorizationViewModel viewModel) : base()
	{
		this.BindingContext = viewModel;
		this.InitializeComponent();
        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
    }
    protected override void OnAppearing() => MainThread.BeginInvokeOnMainThread(async () =>
    {
        await Task.Run(() => { while (!this.isPageLoaded) ; });
        await Task.WhenAll(new Task[]
        {
            this.LoginPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.LoginPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.LoginPanel.Opacity, this.LoginPanel.Scale) = (1.0, 1.0);
    });
    protected override void OnDisappearing() => (this.LoginPanel.Opacity, this.LoginPanel.Scale) = (0, 1.5);

    protected virtual async void LoginButton_Clicked(object sender, EventArgs args)
    {
        if (!this.LoginTextField.IsValidated || !this.PasswordTextField.IsValidated)
        {
            await this.DisplayAlert("Произошла ошибка", "Неверно заполнены поля", "Назад");
        }
    }
}