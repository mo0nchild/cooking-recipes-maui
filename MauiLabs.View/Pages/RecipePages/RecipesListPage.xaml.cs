using MauiLabs.View.Commons.ViewModels.RecipesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.RecipePages;

public partial class RecipesListPage : ContentPage
{
    protected internal readonly RecipesListViewModel viewModel = default!;
    public RecipesListPage(RecipesListViewModel viewModel) : base()
    {
        this.InitializeComponent();
        this.BindingContext = this.viewModel = viewModel;

        this.viewModel.RecipesReloaded += delegate(object sender, EventArgs args)
        {
            this.Dispatcher.Dispatch(() => this.RecipesListView.ItemsSource = this.viewModel.CookingRecipes);
        };
    }
    protected override void OnAppearing() => MainThread.BeginInvokeOnMainThread(() =>
    {
        this.viewModel.LoadRecipesListCommand.Execute(this);
    });
    protected override void OnDisappearing() => MainThread.BeginInvokeOnMainThread(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}