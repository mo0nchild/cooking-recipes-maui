using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace MauiLabs.View.Pages.ProfilePages;

public partial class FriendInfoPage : ContentPage, INotifyPropertyChanged, INavigationService.IQueryableNavigation
{
    protected internal CancellationTokenSource cancellationSource = new();
    protected internal readonly INavigationService navigationService = default!;

    protected internal readonly IUserProfile userProfile = default!;
    protected internal readonly IFriendsList friendProfile = default!;

    public ICommand CancelCommand { get; protected set; } = default!;

    public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";
    public FriendInfoPage(IUserProfile userProfile, INavigationService navigationService, IFriendsList friendProfile) : base()
	{
		this.InitializeComponent();
        (this.userProfile, this.navigationService, this.friendProfile) = (userProfile, navigationService, friendProfile);

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

    protected async void LaunchÑancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
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

    }, async (errorInfo) =>
    {
        await Shell.Current.Navigation.PopAsync(animated: true);
    });
    protected virtual async Task DeleteFriend() => await UserManager.SendRequest(async (token) =>
    {
        if(await this.DisplayAlert("Ïîäòâåğæäåíèå", "Óäàëèòü äğóãà èç ñïèñêà?", "Îê", "Íàçàä"))
        {
            await this.friendProfile.DeleteFriend(new RequestInfo<DeleteFriendRequestModel>()
            {
                RequestModel = new DeleteFriendRequestModel() { RecordId = this.RecordId },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
        }
    });

    protected virtual void BookmarkButton_Clicked(object sender, EventArgs args)
    {

    }
    protected virtual async void DeleteButton_Clicked(object sender, EventArgs args)
    {
        await this.Dispatcher.DispatchAsync(async () =>
        {
            this.LaunchÑancelableTask(() => this.DeleteFriend());
            await Shell.Current.Navigation.PopAsync(animated: true);
        });
    }
    protected virtual void RefreshButton_Clicked(object sender, EventArgs args) => this.OnAppearing();

    protected override void OnAppearing() => this.Dispatcher.Dispatch(async () =>
    {
        this.ReloadProfileImage(this.FileToByteArray(DefaultProfileImage));
        this.LaunchÑancelableTask(() => this.GetFriendInfo());
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