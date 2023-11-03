using MauiLabs.Dal.Entities;
using MauiLabs.View.ViewModels;

namespace MauiLabs.View.Pages;

public partial class UserListPage : ContentPage
{
    public UserListVm ViewModel { get; set; } = default!;

    public UserListPage(UserListVm userProfileVm) : base()
	{
		this.InitializeComponent();
		this.BindingContext = this.ViewModel = userProfileVm;

		this.userProfileList.SelectionChangedCommand = this.ViewModel.SelectProfile;
	}

    private async void userProfileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert("Test", "Test", "Exit");
    }
}