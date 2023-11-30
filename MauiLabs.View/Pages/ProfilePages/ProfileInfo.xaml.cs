using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class ProfileInfo : ContentPage
{
	public ProfileInfo() : base()
	{
		this.InitializeComponent();
	}

    private async void ExitProfile_Clicked(object sender, EventArgs args)
    {
		await UserManager.LogoutUser();
        await Application.Current!.Dispatcher.DispatchAsync(async () =>
        {
            await Shell.Current.Navigation.PopToRootAsync();
            await Shell.Current.GoToAsync(UserManager.AuthorizationRoute);
        });
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Console.WriteLine(Application.Current.MainPage.Navigation.NavigationStack);
    }
}