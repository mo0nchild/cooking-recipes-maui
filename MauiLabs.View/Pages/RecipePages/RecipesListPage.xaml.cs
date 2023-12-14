using MauiLabs.View.Commons.ViewModels.RecipesViewModels;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;

namespace MauiLabs.View.Pages.RecipePages;

public partial class RecipesListPage : ContentPage
{
    protected internal readonly RecipesListViewModel viewModel = default!;
    protected internal readonly INavigationService navigationService = default!;

    protected internal bool isPageLoaded = default!;
    public RecipesListPage(RecipesListViewModel viewModel, INavigationService navigationService) : base()
    {
        this.InitializeComponent();
        (this.navigationService, this.BindingContext) = (navigationService, this.viewModel = viewModel);
        this.viewModel.CategoriesReload += delegate (object sender, EventArgs args)
        {
            this.Dispatcher.Dispatch(() =>
            {
                this.CategoriesPicker.ItemsSource = this.viewModel.Categories;
                if (this.CategoriesPicker.Items.Count > 0)
                {
                    if (!this.viewModel.Categories.Contains(this.viewModel.Category)) this.CategoriesPicker.SelectedIndex = default!;
                    else this.CategoriesPicker.SelectedIndex = this.CategoriesPicker.Items.IndexOf(this.viewModel.Category);
                }
            });
        };
        this.viewModel.RecipesReload += delegate(object sender, EventArgs args)
        {
            this.Dispatcher.Dispatch(() => this.RecipesListView.ItemsSource = this.viewModel.CookingRecipes);
        };
        this.Loaded += delegate (object sender, EventArgs args) { this.isPageLoaded = true; };
    }
    protected virtual async void RecipesListView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (this.viewModel.IsLoading) return;
        var queryParam = new Dictionary<string, object>()
        {
            ["RecipeId"] = ((GetRecipeResponseModel)this.RecipesListView.SelectedItem).Id,
            ["EnablePublisher"] = true,
        };
        await this.navigationService.NavigateToPage<RecipeInfoPage>(Shell.Current, queryParam);
    }
    protected virtual void BackButton_Clicked(object sender, EventArgs args)
    {
        if (this.viewModel.PageIndex <= 1) return;
        this.viewModel.PageIndex--;
        this.viewModel.GetRecipesListCommand.Execute(false);
    }
    protected virtual void NextButton_Clicked(object sender, EventArgs args)
    {
        if (this.viewModel.PageIndex >= this.viewModel.PageCount) return;
        this.viewModel.PageIndex++;
        this.viewModel.GetRecipesListCommand.Execute(false);
    }
    protected virtual void CategoriesPicker_SelectedIndexChanged(object sender, EventArgs args)
    {
        if (this.CategoriesPicker.SelectedItem == null) return;
        this.viewModel.Category = (this.CategoriesPicker.SelectedItem as string) ?? RecipesListViewModel.DefaultCategory;
    }
    protected virtual void SortingTypePicker_SelectedIndexChanged(object sender, EventArgs eargs)
    {
        this.viewModel.SortingType = (this.SortingTypePicker.SelectedItem as string) ?? RecipesListViewModel.DefaultSortingType;
    }
    protected virtual void FilterTextField_Completed(object sender, EventArgs args) => this.ReloadButton_Clicked(this, new EventArgs());
    protected virtual void ReloadButton_Clicked(object sender, EventArgs args)
    {
        this.viewModel.GetCategoriesListCommand.Execute(this);
        this.viewModel.PageIndex = 1;
        this.viewModel.GetRecipesListCommand.Execute(this);
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.ReloadButton_Clicked(this, new EventArgs());

        this.SortingTypePicker.ItemsSource = this.viewModel.SortingTypes;
        this.CategoriesPicker.ItemsSource = this.viewModel.Categories;
        (this.CategoriesPicker.SelectedIndex, this.SortingTypePicker.SelectedIndex) = (default!, default!);

        await Task.Run(() => { while (!this.isPageLoaded); });
        await Task.WhenAll(new Task[]
        {
            this.RecipesListPanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.RecipesListPanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.RecipesListPanel.Opacity, this.RecipesListPanel.Scale) = (1.0, 1.0);
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.viewModel.CancelCommand.Execute(this);
        this.viewModel.Category = PublishedListViewModel.DefaultCategory;

        (this.RecipesListPanel.Opacity, this.RecipesListPanel.Scale) = (0, 1.5);
        this.FilterTextField.Text = string.Empty;
        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}