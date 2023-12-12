using MauiLabs.View.Commons.ViewModels.RecipesViewModels;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Interfaces;

namespace MauiLabs.View.Pages.RecipePages;

public partial class PublishedListPage : ContentPage
{
	protected internal readonly PublishedListViewModel viewModel = default!;
    protected internal readonly INavigationService navigationService = default!;

    protected internal bool isPageLoaded = default!;
    public PublishedListPage(PublishedListViewModel viewModel, INavigationService navigationService) : base()
	{
		this.InitializeComponent();
        (this.navigationService, this.BindingContext) = (navigationService, this.viewModel = viewModel);
        this.viewModel.RecipesReload += delegate (object sender, EventArgs args)
        {
            this.Dispatcher.Dispatch(() => this.RecipesListView.ItemsSource = this.viewModel.CookingRecipes);
        };
        this.viewModel.DisplayInfo += (sender, message) => this.Dispatcher.Dispatch(async () =>
        {
            await this.DisplayAlert("Действие выполнено", message, "Назад");
        });
        this.viewModel.CheckСonfirm += (message) => this.DisplayAlert("Подтверждение", message, "Ок", "Назад");
        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
    }

    protected virtual async void RecipesListView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        var queryParam = new Dictionary<string, object>()
        {
            ["RecipeId"] = ((GetRecipeResponseModel)this.RecipesListView.SelectedItem).Id
        };
        await this.navigationService.NavigateToPage<EditRecipePage>(Shell.Current, queryParam);
    }
    protected virtual async void AddRecipeButton_Clicked(object sender, EventArgs args)
    {
        var queryParam = new Dictionary<string, object>() { ["RecipeId"] = -1 };
        await this.navigationService.NavigateToPage<EditRecipePage>(Shell.Current, queryParam);
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.GetPublishedListCommand.Execute(this);
        await Task.Run(() => { while (!this.isPageLoaded) ; });
        await Task.WhenAll(new Task[]
        {
            this.PublishedListPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.PublishedListPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.PublishedListPanel.Opacity, this.PublishedListPanel.Scale) = (1.0, 1.0);
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);

        (this.PublishedListPanel.Opacity, this.PublishedListPanel.Scale) = (0, 1.5);
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}