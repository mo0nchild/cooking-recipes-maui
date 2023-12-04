using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class ProfileInfoPage : ContentPage
{
    protected internal readonly ProfileInfoViewModel viewModel = default!;
    public ProfileInfoPage(ProfileInfoViewModel viewModel) : base()
	{
		this.InitializeComponent();
        this.BindingContext = this.viewModel = viewModel;
    }
    protected override async void OnAppearing() => await this.Dispatcher.DispatchAsync(() =>
    {
        this.viewModel.GetProfileCommand.Execute(this);
    });
    protected override async void OnDisappearing() => await this.Dispatcher.DispatchAsync(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
    protected virtual async void ExitProfile_Clicked(object sender, EventArgs args)
    {
        await UserManager.LogoutUser();
        await Application.Current!.Dispatcher.DispatchAsync(async () =>
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync(UserManager.AuthorizationRoute);
        });
    }
    protected virtual async void UpdateButton_Clicked(object sender, EventArgs args)
    {
        await this.Dispatcher.DispatchAsync(() => this.viewModel.GetProfileCommand.Execute(this));
    }
    protected virtual async void ReferenceLinkButton_Clicked(object sender, EventArgs e)
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = string.Format("Используйте ссылку в приложение: {0}\n", this.viewModel.ReferenceLink), 
            Title = "Ссылка для добавление в друзья", Uri = @"https://github.com/mo0nchild/cs-maui-labs",
        });
    }
}