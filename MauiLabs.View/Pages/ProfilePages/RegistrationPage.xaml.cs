using MauiLabs.View.Commons.ContentViews;
using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class RegistrationPage : ContentPage
{
    protected internal readonly RegistrationViewModel viewModel = default!;

    protected internal bool isPageLoaded = default!;
    public RegistrationPage(RegistrationViewModel viewModel) : base()
	{
		this.InitializeComponent();
        this.BindingContext = this.viewModel = viewModel;

        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
        this.viewModel.DisplayAlert += this.DiplayAlertHandler;
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        if (await UserManager.JwtToken() != null)
        {
            this.OnDisappearing();
            await Shell.Current.GoToAsync("//recipes", true);
        }
        await Task.Run(() => { while (!this.isPageLoaded) ; });
        await Task.WhenAll(new Task[]
        {
            this.RegisterPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.RegisterPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.RegisterPanel.Opacity, this.RegisterPanel.Scale) = (1.0, 1.0);
    });
    protected override async void OnDisappearing() => await this.Dispatcher.DispatchAsync(async () =>
    {
        this.viewModel.CancelCommand.Execute(null);
        (this.RegisterPanel.Opacity, this.RegisterPanel.Scale) = (0, 1.5);

        this.PasswordTextField.TextValue = string.Empty;
        this.LoginTextField.TextValue = string.Empty;

        this.NameTextField.TextValue = string.Empty;
        this.SurnameTextField.TextValue = string.Empty;
        this.EmailTextField.TextValue = string.Empty;

        this.Expander.ResetExpander();
        this.viewModel.UserImage = null;
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
    protected virtual async void DiplayAlertHandler(object sender, string message)
    {
        await this.DisplayAlert("Произошла ошибка", message, "Назад");
    }
    protected virtual async void RegisterButton_Clicked(object sender, EventArgs args)
    {
        var validationState = this.viewModel.ValidationState;
        if (validationState.Where(item => !item.Value).Count() > 0)
        {
            await this.DisplayAlert("Произошла ошибка", "Неверно заполнены поля", "Назад");
        } 
    }
}