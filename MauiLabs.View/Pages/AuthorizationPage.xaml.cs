using MauiLabs.View.ViewModels.ProfilesViewModels;

namespace MauiLabs.View.Pages;

public partial class AuthorizationPage : ContentPage
{
    public AuthorizationPage(AuthorizationViewModel viewModel) : base()
	{
		this.InitializeComponent();
		this.BindingContext = viewModel;
    }

    protected override void OnAppearing() => base.OnAppearing();
}