using MauiLabs.View.Pages.RecipePages;
using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class BookmarksListPage : ContentPage, INotifyPropertyChanged
{
    protected internal readonly INavigationService navigationService = default!;
    protected internal readonly IBookmarksList bookmarksService = default!;
    protected internal readonly ICookingRecipes cookingRecipes = default!;
    protected internal CancellationTokenSource cancellationSource = new();

    public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";
    public static readonly string DefaultCategory = "����� ���������";

    public ICommand GetBookmarksListCommand { get; protected set; } = default!;
    public ICommand DeleteBookmarkCommand { get; protected set; } = default!;
    public ICommand CancelCommand { get; protected set; } = default!;

    public event EventHandler CategoriesReload = delegate { };
    public event EventHandler RecipesReload = delegate { };
    public BookmarksListPage(IBookmarksList bookmarksService, ICookingRecipes cookingRecipes, INavigationService navigationService) : base()
	{
		this.InitializeComponent();
        (this.bookmarksService, this.navigationService, this.cookingRecipes) = (bookmarksService, navigationService, cookingRecipes);
        this.RecipesReload += (sender, args) => this.Dispatcher.Dispatch(() => this.RecipesListView.ItemsSource = this.CookingRecipes);
        this.CategoriesReload += (sender, args) => this.Dispatcher.Dispatch(() =>
        {
            this.CategoriesPicker.ItemsSource = this.Categories;
            if (this.CategoriesPicker.Items.Count > 0)
            {
                if (!this.Categories.Contains(this.Category)) this.CategoriesPicker.SelectedIndex = default!;
                else this.CategoriesPicker.SelectedIndex = this.CategoriesPicker.Items.IndexOf(this.Category);
            }
        });
        this.GetBookmarksListCommand = new Command(() => this.Launch�ancelableTask(() => this.GetBookmarksListCommandHandler()));
        this.DeleteBookmarkCommand = new Command<int>(async (id) =>
        {
            if (await this.DisplayAlert("�������������", "������� ������ �������?", "��", "�����"))
            {
                this.Launch�ancelableTask(() => this.DeleteBookmarkCommandHandler(id));
            }
        });
        this.CancelCommand = new Command(this.CancelCommandHandler);
    }
    private protected string textFilter = string.Empty;
    public string TextFilter { get => this.textFilter; set { this.textFilter = value; OnPropertyChanged(); } }
    public string Category { get; set; } = DefaultCategory;

    private protected int allCount = default!;
    public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

    private protected bool isEmpty = default!;
    public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }

    private protected bool isLoading = default;
    public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

    public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = new();
    public ObservableCollection<string> Categories { get; protected set; } = new() { DefaultCategory };

    protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
    {
        if (this.isLoading == false) return;
        this.cancellationSource.Cancel();

        this.cancellationSource = new CancellationTokenSource();
        (this.IsLoading) = (default);
    });
    protected async void Launch�ancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
    {
        this.IsLoading = true; await cancelableTask.Invoke();
        this.IsLoading = false;
    });
    public virtual byte[] FileToByteArray(string filename)
    {
        using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
        {
            using var binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
    protected virtual async Task GetBookmarksListCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        var requestResult = await this.bookmarksService.GetBookmarks(new RequestInfo<GetBookmarksRequestModel>()
        {
            RequestModel = new GetBookmarksRequestModel()
            {
                TextFilter = this.TextFilter == string.Empty ? null : this.TextFilter,
                Category = this.Category == DefaultCategory ? null : this.Category,
            },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        foreach (var bookmarkrecord in requestResult.Bookmarks)
        {
            bookmarkrecord.Recipe.Image = bookmarkrecord.Recipe.Image.Length != 0
                ? bookmarkrecord.Recipe.Image : this.FileToByteArray(DefaultRecipeImage);
        }
        this.CookingRecipes = new(requestResult.Bookmarks.Select(p => p.Recipe));
        this.AllCount = requestResult.AllCount;

        this.RecipesReload.Invoke(this, new EventArgs());
        var categoriesResult = await this.cookingRecipes.GetCategoriesList(token, this.cancellationSource.Token);

        this.Categories = new(categoriesResult.Categories);
        this.Categories.Insert(0, DefaultCategory);

        this.CategoriesReload.Invoke(this, new EventArgs());
    }, (errorInfo) =>
    {
        (this.Categories, this.CookingRecipes) = (new() { DefaultCategory }, new());
        this.AllCount = default!;

        this.RecipesReload.Invoke(this, new EventArgs());
        this.CategoriesReload.Invoke(this, new EventArgs());
    });
    protected virtual async Task DeleteBookmarkCommandHandler(int id) => await UserManager.SendRequest(async (token) =>
    {
        await this.bookmarksService.DeleteBookmark(new RequestInfo<DeleteBookmarkRequestModel>()
        {
            RequestModel = new DeleteBookmarkRequestModel() { RecipeId = id },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        await this.GetBookmarksListCommandHandler();
        this.Dispatcher.Dispatch(async () => await this.DisplayAlert("�������� ��������", "������� �������", "�����"));
    });

    protected virtual async void RecipesListView_SelectionChanged(object sender, SelectionChangedEventArgs args)
    {
        if (this.IsLoading) return;
        var queryParam = new Dictionary<string, object>()
        {
            ["RecipeId"] = ((GetRecipeResponseModel)this.RecipesListView.SelectedItem).Id,
            ["EnablePublisher"] = true,
        };
        await this.navigationService.NavigateToPage<RecipeInfoPage>(Shell.Current, queryParam);
    }
    protected virtual void RefreshButton_Clicked(object sender, EventArgs args) => this.GetBookmarksListCommand.Execute(null);
    protected virtual void CategoriesPicker_SelectedIndexChanged(object sender, EventArgs args)
    {
        if (this.CategoriesPicker.SelectedItem == null) return;
        this.Category = (this.CategoriesPicker.SelectedItem as string) ?? DefaultCategory;
    }
    protected virtual void FilterTextField_Completed(object sender, EventArgs args)
    {
        this.GetBookmarksListCommand.Execute(null);
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(() =>
    {
        this.GetBookmarksListCommand.Execute(this);
        this.CategoriesPicker.ItemsSource = this.Categories;

        this.CategoriesPicker.SelectedIndex = default!;
    });
    protected override void OnDisappearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.CancelCommand.Execute(this);
        (this.Category, this.FilterTextField.Text) = (DefaultCategory, string.Empty);

        await this.PageScroller.ScrollToAsync(0, 0, false);
    });
}