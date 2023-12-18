using MauiLabs.View.Pages.RecipePages;
using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class FriendInfoPage : ContentPage, INotifyPropertyChanged, INavigationService.IQueryableNavigation
{
    protected internal CancellationTokenSource cancellationSource = new();
    protected internal readonly INavigationService navigationService = default!;
    protected internal readonly IBookmarksList bookmarksService = default!;

    protected internal readonly IUserProfile userProfile = default!;
    protected internal readonly IFriendsList friendProfile = default!;

    public ICommand GetBookmarksListCommand { get; protected set; } = default!;
    public ICommand CancelCommand { get; protected set; } = default!;

    public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";
    public static readonly string DefaultRecipeImage = $"MauiLabs.View.Resources.Images.Recipe.defaultrecipe.jpg";

    public event EventHandler RecipesReload = delegate { };
    public FriendInfoPage(IUserProfile userProfile, IBookmarksList bookmarksService, IFriendsList friendProfile, 
        INavigationService navigationService) : base()
	{
		this.InitializeComponent();
        (this.userProfile, this.bookmarksService, this.friendProfile) = (userProfile, bookmarksService, friendProfile);
        this.navigationService = navigationService;
        this.RecipesReload += (sender, args) => 
        {
            this.Dispatcher.Dispatch(() => this.RecipesListView.ItemsSource = this.CookingRecipes);
        };
        this.CancelCommand = new Command(this.CancelCommandHandler);
	}
    private protected bool isLoading = default;
    public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

    private protected string friendName = default!;
    public string FriendName { get => this.friendName; set { this.friendName = value; OnPropertyChanged(); } }

    private protected string friendSurname = default!;
    public string FriendSurname { get => this.friendSurname; set { this.friendSurname = value; OnPropertyChanged(); } }

    private protected string friendEmail = default!;
    public string FriendEmail { get => this.friendEmail; set { this.friendEmail = value; OnPropertyChanged(); } }

    public virtual int FriendId { get; protected set; } = default!;
    public virtual int RecordId { get; protected set; } = default!;


    private protected int allCount = default!;
    public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

    private protected bool isEmpty = default!;
    public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }
    public ObservableCollection<GetRecipeResponseModel> CookingRecipes { get; protected set; } = new();

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
    protected virtual void ReloadProfileImage(byte[] image) => this.Dispatcher.Dispatch(() =>
    {
        this.ProfileImage.Content = new Image()
        {
            Source = ImageSource.FromStream(() => new MemoryStream(image)),
            Margin = Thickness.Zero, Aspect = Aspect.AspectFill,
        };
    });
    protected virtual async Task GetFriendInfo() => await UserManager.SendRequest(async (token) =>
    {
        var requestResult = await this.userProfile.GetProfileInfo(token, this.FriendId, this.cancellationSource.Token);
        if (requestResult.Image.Length == 0)
        {
            this.ReloadProfileImage(this.FileToByteArray(DefaultProfileImage));
        }
        else this.ReloadProfileImage(requestResult.Image);
        (this.FriendName, this.FriendSurname) = (requestResult.Name, requestResult.Surname);
        this.FriendEmail = requestResult.Email;

        var bookmarkRequest = await this.bookmarksService.GetBookmarksById(new RequestInfo<GetBookmarksByIdRequestModel>() 
        { 
            RequestModel = new GetBookmarksByIdRequestModel() { ProfileId = this.FriendId },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        foreach (var bookmarkrecord in bookmarkRequest.Bookmarks)
        {
            bookmarkrecord.Recipe.Image = bookmarkrecord.Recipe.Image.Length != 0
                ? bookmarkrecord.Recipe.Image : this.FileToByteArray(DefaultRecipeImage);
        }
        this.CookingRecipes = new(bookmarkRequest.Bookmarks.Select(p => p.Recipe));
        this.AllCount = bookmarkRequest.AllCount;

        this.RecipesReload.Invoke(this, new EventArgs());
    }, async (errorInfo) =>
    {
        await Shell.Current.Navigation.PopAsync(animated: true);
    });
   
    protected virtual async Task DeleteFriend() => await UserManager.SendRequest(async (token) =>
    {
        if(await this.DisplayAlert("œÓ‰Ú‚ÂÊ‰ÂÌËÂ", "”‰‡ÎËÚ¸ ‰Û„‡ ËÁ ÒÔËÒÍ‡?", "ŒÍ", "Õ‡Á‡‰"))
        {
            await this.friendProfile.DeleteFriend(new RequestInfo<DeleteFriendRequestModel>()
            {
                RequestModel = new DeleteFriendRequestModel() { RecordId = this.RecordId },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
        }
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
    protected virtual async void DeleteButton_Clicked(object sender, EventArgs args)
    {
        await this.Dispatcher.DispatchAsync(async () =>
        {
            this.Launch—ancelableTask(() => this.DeleteFriend());
            await Shell.Current.Navigation.PopAsync(animated: true);
        });
    }
    protected virtual void RefreshButton_Clicked(object sender, EventArgs args) => this.OnAppearing();

    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.ReloadProfileImage(this.FileToByteArray(DefaultProfileImage));
        this.Launch—ancelableTask(() => this.GetFriendInfo());
        await Task.WhenAll(new Task[]
        {
            this.ProfilePanel.ScaleTo(1.0, length: 600, easing: Easing.SinInOut),
            this.ProfilePanel.FadeTo(1.0, length: 600, easing: Easing.SinInOut),
        });
        (this.ProfilePanel.Opacity, this.ProfilePanel.Scale) = (1.0, 1.0);
    });
    public virtual void SetNavigationQuery(IDictionary<string, object> queries)
    {
        (this.FriendId, this.RecordId) = ((int)queries["FriendId"], (int)queries["RecordId"]);
    }
    protected override void OnDisappearing() => base.OnDisappearing();
}