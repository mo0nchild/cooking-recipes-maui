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
    }

    protected override void OnAppearing() => MainThread.BeginInvokeOnMainThread(() =>
    {
        this.viewModel.LoadRecipesListCommand.Execute(this);
    });

    protected override void OnDisappearing() => MainThread.BeginInvokeOnMainThread(() =>
    {
        this.viewModel.CancelCommand.Execute(this);
    });
}