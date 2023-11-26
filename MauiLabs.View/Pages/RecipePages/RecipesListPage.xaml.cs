using MauiLabs.View.Commons.ViewModels.RecipesViewModels;
using MauiLabs.View.Services.Commons;

namespace MauiLabs.View.Pages.RecipePages;

public partial class RecipesListPage : ContentPage
{
	public RecipesListPage(RecipesListViewModel viewModel) : base()
	{
		this.InitializeComponent();
		this.BindingContext = viewModel;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var shell = Shell.Current.Navigation;
        await UserManager.SendRequest((token) =>
        {
            throw new ViewServiceException("shit", System.Net.HttpStatusCode.Unauthorized);
        });
        Console.WriteLine();
    }
}