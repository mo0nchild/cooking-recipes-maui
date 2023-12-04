using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class AuthorizationPage : ContentPage
{
    protected internal readonly AuthorizationViewModel viewModel = default!;

    protected internal bool isPageLoaded = default!;
    public AuthorizationPage(AuthorizationViewModel viewModel) : base()
	{
		this.InitializeComponent(); 
		this.BindingContext = this.viewModel = viewModel;

        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        if (await UserManager.JwtToken() != null)
        {
            this.OnDisappearing();
            await Shell.Current.GoToAsync("//recipes", true);
        }
        await Task.Run(() => { while (!this.isPageLoaded); });
        await Task.WhenAll(new Task[]
        {
            this.LoginPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.LoginPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.LoginPanel.Opacity, this.LoginPanel.Scale) = (1.0, 1.0);
    });
    protected override async void OnDisappearing()
    {
        this.viewModel.CancelCommand.Execute(null);
        (this.LoginPanel.Opacity, this.LoginPanel.Scale) = (0, 1.5);

        this.PasswordTextField.TextValue = string.Empty;
        this.LoginTextField.TextValue = string.Empty;
        await this.PageScroller.ScrollToAsync(0, 0, false);
    }
    protected virtual async void LoginButton_Clicked(object sender, EventArgs args)
    {
        if (!this.LoginTextField.IsValidated || !this.PasswordTextField.IsValidated)
        {
            await this.DisplayAlert("Произошла ошибка", "Неверно заполнены поля", "Назад");
        }
    }
}