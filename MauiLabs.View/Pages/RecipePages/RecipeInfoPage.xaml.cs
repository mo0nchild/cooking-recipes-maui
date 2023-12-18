using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace MauiLabs.View.Pages.RecipePages;

public partial class RecipeInfoPage : ContentPage, INotifyPropertyChanged, INavigationService.IQueryableNavigation
{
    protected internal CancellationTokenSource cancellationSource = new();
    protected internal readonly ICookingRecipes cookingRecipes = default!;
    protected internal readonly IBookmarksList bookmarks = default!;
    protected internal readonly INavigationService navigationService = default!;

    public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";
    public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";
    public virtual double ImageSize { get => 96; }

    public ICommand GetRecipeCommand { get; protected set; } = default!;
    public ICommand CancelCommand { get; protected set; } = default!;

    public RecipeInfoPage(ICookingRecipes cookingRecipes, IBookmarksList bookmarks, INavigationService navigationService) : base()
    {
        this.InitializeComponent();
        (this.cookingRecipes, this.navigationService, this.bookmarks) = (cookingRecipes, navigationService, bookmarks);
        this.GetRecipeCommand = new Command(() =>
        {
            this.Launch—ancelableTask(() => this.GetRecipeCommandHandler());
        });
        this.CancelCommand = new Command(this.CancelCommandHandler);
    }
    private protected bool isLoading = default;
    public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

    private protected string recipeName = default!;
    public string RecipeName { get => this.recipeName; set { this.recipeName = value; OnPropertyChanged(); } }

    private protected string recipeCategory = default!;
    public string RecipeCategory { get => this.recipeCategory; set { this.recipeCategory = value; OnPropertyChanged(); } }

    private protected string description = default!;
    public string Description { get => this.description; set { this.description = value; OnPropertyChanged(); } }

    private protected byte[] recipeImage = new byte[0];
    public byte[] RecipeImage { get => this.recipeImage; set { this.recipeImage = value; OnPropertyChanged(); } }

    private protected byte[] publisherImage = new byte[0];
    public byte[] PublisherImage { get => this.publisherImage; set { this.publisherImage = value; OnPropertyChanged(); } }

    private protected string publisherName = default!;
    public string PublisherName { get => this.publisherName; set { this.publisherName = value; OnPropertyChanged(); } }

    private protected string publisherSurname = default!;
    public string PublisherSurname { get => this.publisherSurname; set { this.publisherSurname = value; OnPropertyChanged(); } }

    private protected string publisherTime = default!;
    public string PublisherTime { get => this.publisherTime; set { this.publisherTime = value; OnPropertyChanged(); } }

    public sealed class IngredientModel : object
    {
        public required string Name { get; set; } = default!;
        public required string Unit { get; set; } = default!;
        public required string Value { get; set; } = default!;
    }
    private protected bool ingredientsEmpty = true;
    public bool IngredientsEmpty { get => this.ingredientsEmpty; set { this.ingredientsEmpty = value; OnPropertyChanged(); } }
    public virtual ObservableCollection<IngredientModel> Ingredients { get; protected set; } = new();
    public virtual int RecipeId { get; protected set; } = default!;
    public virtual bool EnablePublisher { get; protected set; } = default!;

    protected async void Launch—ancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
    {
        this.IsLoading = true; await cancelableTask.Invoke();
        this.IsLoading = false;
    });
    protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
    {
        if (this.isLoading == false) return;

        this.cancellationSource.Cancel();
        this.cancellationSource = new CancellationTokenSource();

        (this.IsLoading) = (default);
    });
    public virtual byte[] FileToByteArray(string filename)
    {
        using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
        {
            using var binaryReader = new BinaryReader(fileStream);
            return binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
    protected virtual void DisplayMessage(string title, string message)
    {
        this.Dispatcher.Dispatch(async () => await this.DisplayAlert(title, message, "Õ‡Á‡‰"));
    }
    protected virtual void ReloadProfileImage(byte[] image, Border border) => this.Dispatcher.Dispatch(() =>
    {
        border.Content = new Image()
        {
            Source = ImageSource.FromStream(() => new MemoryStream(image)),
            Margin = Thickness.Zero, Aspect = Aspect.AspectFill,
        };
    });
    protected virtual void RecommendButton_Clicked(object sender, EventArgs args)
    {

    }
    protected virtual void BookmarkButton_Clicked(object sender, EventArgs args)
    {
        this.Launch—ancelableTask(() => UserManager.SendRequest(async token =>
        {
            await this.bookmarks.AddBookmark(new RequestInfo<AddBookmarkRequestModel>() 
            { 
                RequestModel = new AddBookmarkRequestModel() { RecipeId = this.RecipeId },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
        }));
    }
    protected virtual void PublisherRecipesButton_Clicked(object sender, EventArgs args)
    {

    }
    protected virtual async void CommentsButton_Clicked(object sender, EventArgs args)
    {
        var queryParam = new Dictionary<string, object>() { ["RecipeId"] = this.RecipeId };
        await this.navigationService.NavigateToPage<CommentsListPage>(Shell.Current, queryParam);
    }
    protected virtual async Task GetRecipeCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        var recipeResult = await this.cookingRecipes.GetRecipeInfo(new RequestInfo<GetRecipeRequestModel>()
        {
            RequestModel = new GetRecipeRequestModel() { RecipeId = this.RecipeId },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        await this.Dispatcher.DispatchAsync(() =>
        {
            if (recipeResult.Image.Length <= 0)
            {
                this.ReloadProfileImage(this.FileToByteArray(DefaultRecipeImage), this.RecipeImageContent);
                this.RecipeImage = null;
            }
            else this.ReloadProfileImage(this.RecipeImage = recipeResult.Image, this.RecipeImageContent);
            (this.RecipeName, this.Description) = (recipeResult.Name, recipeResult.Description);
            this.RecipeCategory = recipeResult.Category;

            if (recipeResult.Publisher.Image.Length <= 0)
            {
                this.ReloadProfileImage(this.FileToByteArray(DefaultProfileImage), this.PublisherImageContent);
                this.RecipeImage = null;
            }
            else this.ReloadProfileImage(this.RecipeImage = recipeResult.Publisher.Image, this.PublisherImageContent);
            (this.PublisherName, this.PublisherSurname) = (recipeResult.Publisher.Name, recipeResult.Publisher.Surname);
            this.PublisherTime = recipeResult.PublicationTime.ToString();

            if (recipeResult.Ingredients.Count > 0)
            {
                this.Ingredients = new(recipeResult.Ingredients.Select(item => new IngredientModel()
                {
                    Name = item.Name, Unit = item.Unit, Value = item.Value.ToString()
                }));
                (this.IngredientsView.ItemsSource, this.IngredientsEmpty) = (this.Ingredients, default!);
            }
        });
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));
    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.ReloadProfileImage(this.FileToByteArray(DefaultRecipeImage), this.RecipeImageContent);
        this.ReloadProfileImage(this.FileToByteArray(DefaultProfileImage), this.PublisherImageContent);
        this.GetRecipeCommand.Execute(null);
        await Task.WhenAll(new Task[]
        {
            this.RecipePanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.RecipePanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.RecipePanel.Opacity, this.RecipePanel.Scale) = (1.0, 1.0);
    });
    public virtual void SetNavigationQuery(IDictionary<string, object> queries)
    {
        (this.RecipeId, this.EnablePublisher) = ((int)queries["RecipeId"], (bool)queries["EnablePublisher"]);
    }
    protected override void OnDisappearing() => base.OnDisappearing();
}