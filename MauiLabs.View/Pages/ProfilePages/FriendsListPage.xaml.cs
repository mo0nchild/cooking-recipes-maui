 using MauiLabs.View.Commons.ViewModels.ProfilesViewModels;
using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;
using MauiLabs.View.Services.Interfaces;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class FriendsListPage : ContentPage
{
    protected internal readonly FriendsListViewModel viewModel = default!;
    protected internal readonly INavigationService navigationService = default!;

    protected internal bool isPageLoaded = default!;
    public FriendsListPage(FriendsListViewModel viewModel, INavigationService navigationService) : base()
	{
		this.InitializeComponent();
        (this.navigationService, this.BindingContext) = (navigationService, this.viewModel = viewModel);
        this.viewModel.FriendsReload += delegate (object sender, EventArgs args)
        {
            this.Dispatcher.Dispatch(() => this.FriendsListView.ItemsSource = this.viewModel.FriendsList);
        };
        this.viewModel.DisplayInfo += (sender, message) => this.Dispatcher.Dispatch(async () =>
        {
            await this.DisplayAlert("Действие выполнено", message, "Назад");
        });
        this.viewModel.CheckСonfirm += (message) => this.DisplayAlert("Подтверждение", message, "Ок", "Назад");
        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
    }
    protected virtual async void FriendsListView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (this.FriendsListView.SelectedItem is FriendInfoModel friendInfo)
        {
            var queryParam = new Dictionary<string, object>() 
            { 
                ["FriendId"] = friendInfo.Profile.Id, ["RecordId"] = friendInfo.Id, 
            };
            await this.navigationService.NavigateToPage<FriendInfoPage>(Shell.Current, queryParam);
        }
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.GetFriendsListCommand.Execute(this);
        await Task.Run(() => { while (!this.isPageLoaded) ; });
        await Task.WhenAll(new Task[]
        {
            this.FriendsListPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.FriendsListPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.FriendsListPanel.Opacity, this.FriendsListPanel.Scale) = (1.0, 1.0);
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        this.ReferenceTextField.Text = string.Empty;

        (this.FriendsListPanel.Opacity, this.FriendsListPanel.Scale) = (0, 1.5);
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}