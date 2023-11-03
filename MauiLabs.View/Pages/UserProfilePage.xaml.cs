using MauiLabs.View.ViewModels;

namespace MauiLabs.View.Pages;

public partial class UserProfilePage : ContentPage
{
	public UserProfileVm ViewModel { get; set; } = default!;

	public UserProfilePage(UserProfileVm viewModel) : base()
	{
		this.InitializeComponent();
		this.BindingContext = this.ViewModel = viewModel;
	}

    protected override bool OnBackButtonPressed()
    {


        return base.OnBackButtonPressed();
    }
}