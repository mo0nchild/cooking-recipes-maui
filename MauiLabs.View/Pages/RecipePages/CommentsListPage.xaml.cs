using MauiLabs.View.Services.ApiModels.Commons.RecipeModels;
using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Reflection;
using System.Windows.Input;

namespace MauiLabs.View.Pages.RecipePages;

public partial class CommentsListPage : ContentPage, INotifyPropertyChanged, INavigationService.IQueryableNavigation
{
    protected internal CancellationTokenSource cancellationSource = new();
    protected internal readonly ICommentsList commentsList = default!;

    public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";
    public static readonly int RecordsOnPage = 5, RatingScale = 10;
    public virtual double ImageSize { get => 96; }

    public ICommand GetCommentInfoCommand { get; protected set; } = default!;
    public ICommand GetCommentsListCommand { get; protected set; } = default!;

    public ICommand EditCommentInfoCommand { get; protected set; } = default!;
    public ICommand CancelCommand { get; protected set; } = default!;
    public CommentsListPage(ICommentsList commentsList) : base()
	{
		this.InitializeComponent();
        this.commentsList = commentsList;
        this.GetCommentInfoCommand = new Command(() =>
        {
            this.Launch—ancelableTask(() => this.GetCommentInfoCommandHandler());
        });
        this.GetCommentsListCommand = new Command(() => 
        {
            this.Launch—ancelableTask(() => this.GetCommentsListCommandHandler());
        });
        this.EditCommentInfoCommand = new Command(() =>
        {
            this.Launch—ancelableTask(() => this.EditCommentInfoCommandHandler());
        });
        this.CancelCommand = new Command(this.CancelCommandHandler);
    }
    private protected bool isLoading = default;
    public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

    private protected double rating = default!;
    public double Rating { get => this.rating; set { this.rating = value; OnPropertyChanged(); } }

    private protected string text = string.Empty;
    public string Text { get => this.text; set { this.text = value; OnPropertyChanged(); } }

    public ObservableCollection<CommentInfoModel> Comments { get; protected set; } = new();
    
    private protected int pageIndex = default!;
    public int PageIndex { get => this.pageIndex; set { this.pageIndex = value; OnPropertyChanged(); } }

    private protected int pageCount = default!;
    public int PageCount { get => this.pageCount; set { this.pageCount = value; OnPropertyChanged(); } }

    private protected int allCount = default!;
    public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

    private protected bool isEmpty = default!;
    public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }
    public Dictionary<string, bool> ValidationState { get; set; } = new();

    public virtual bool CommentsLoaded { get; protected set; }  = default;
    public virtual int RecipeId { get; protected set; } = default!;

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
    protected virtual async Task GetCommentInfoCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        var requestModel = new RequestInfo<GetCommentRequestModel>()
        {
            RequestModel = new GetCommentRequestModel() { RecipeId = this.RecipeId },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        };
        GetCommentResponseModel commentResult = default!;
        try { commentResult = await this.commentsList.GetCommentInfo(requestModel); }
        catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType != HttpStatusCode.Unauthorized)
        {
            this.CommentsLoaded = default!; return;
        }
        this.CommentsLoaded = true;
        await this.Dispatcher.DispatchAsync(() =>
        {
            (this.Text, this.Rating) = (commentResult.Text, commentResult.Rating);
            this.UnitValueSlider.Value = commentResult.Rating * RatingScale;
        });
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));

    protected virtual async Task GetCommentsListCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        var commentsResult = await this.commentsList.GetCommentsList(new RequestInfo<GetRecipeCommentsRequestModel>()
        {
            RequestModel = new GetRecipeCommentsRequestModel() 
            { 
                RecipeId = this.RecipeId, SortingType = CommentSortingType.ByDate,
                Skip = RecordsOnPage * (this.PageIndex - 1),
                Take = (RecordsOnPage * (this.PageIndex - 1)) + RecordsOnPage,
            },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        await this.Dispatcher.DispatchAsync(() =>
        {
            foreach (var commentsRecord in commentsResult.Comments)
            {
                commentsRecord.Profile.Image = commentsRecord.Profile.Image.Length != 0
                    ? commentsRecord.Profile.Image : this.FileToByteArray(DefaultProfileImage);
            }
            (this.AllCount, this.Comments) = (commentsResult.AllCount, new(commentsResult.Comments));
            this.PageCount = (int)Math.Ceiling(commentsResult.AllCount / (double)RecordsOnPage);

            this.CommentsListView.ItemsSource = this.Comments;
        });
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));

    protected virtual async Task EditCommentInfoCommandHandler() => await UserManager.SendRequest(async (token) =>
    {
        if (this.CommentsLoaded) await this.commentsList.EditComment(new RequestInfo<EditCommentRequestModel>()
        {
            RequestModel = new EditCommentRequestModel() { RecipeId = this.RecipeId, Rating = this.Rating, Text = this.Text, },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        else await this.commentsList.AddComment(new RequestInfo<AddCommentRequestModel>()
        {
            RequestModel = new AddCommentRequestModel() { RecipeId = this.RecipeId, Rating = this.Rating, Text = this.Text, },
            CancelToken = this.cancellationSource.Token, ProfileToken = token,
        });
        this.DisplayMessage("”ÒÔÂ¯ÌÓÂ ‰ÂÈÒÚ‚ËÂ", " ÓÏÏÂÌÚ‡ËÈ ÛÒÔÂ¯ÌÓ ÒÓı‡ÌÂÌ");
        (this.PageIndex, this.CommentsLoaded) = (1, true);
        this.GetCommentsListCommand.Execute(null);
    }, async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true));
    protected virtual void BackButton_Clicked(object sender, EventArgs args)
    {
        if (this.PageIndex <= 1) return;
        this.PageIndex--;
        this.GetCommentsListCommand.Execute(false);
    }
    protected virtual void NextButton_Clicked(object sender, EventArgs args)
    {
        if (this.PageIndex >= this.PageCount) return;
        this.PageIndex++;
        this.GetCommentsListCommand.Execute(false);
    }
    protected virtual void UnitValueSlider_ValueChanged(object sender, ValueChangedEventArgs args) 
    {
        this.Rating = ((int)this.UnitValueSlider.Value / RatingScale);
    }
    protected virtual void AddCommentButton_Clicked(object sender, EventArgs args)
    {
        if (this.ValidationState["Text"]) this.EditCommentInfoCommand.Execute(null);
        else this.DisplayMessage("œÓËÁÓ¯Î‡ Ó¯Ë·Í‡", "ÕÂ‚ÂÌÓÂ Á‡ÔÓÎÌÂÌÓ ÔÓÎÂ ÍÓÏÏÂÌÚ‡Ëˇ");
    }
    protected virtual void DeleteCommentButton_Clicked(object sender, EventArgs args)
    {
        this.Launch—ancelableTask(async () => await UserManager.SendRequest(async (token) =>
        {
            if (!this.CommentsLoaded) return;
            await this.commentsList.DeleteComment(new RequestInfo<DeleteCommentRequestModel>()
            {
                RequestModel = new DeleteCommentRequestModel() { RecipeId = this.RecipeId },
                CancelToken = this.cancellationSource.Token,
                ProfileToken = token,
            });
            await this.Dispatcher.DispatchAsync(() =>
            {
                (this.Text, this.UnitValueSlider.Value, this.PageIndex) = (string.Empty, default!, 1);
            });
            this.CommentsLoaded = default!;
            this.DisplayMessage("”ÒÔÂ¯ÌÓÂ ‰ÂÈÒÚ‚ËÂ", " ÓÏÏÂÌÚ‡ËÈ ÛÒÔÂ¯ÌÓ Û‰‡ÎÂÌ");
            this.GetCommentsListCommand.Execute(null);
        },
        async (errorInfo) => await Shell.Current.Navigation.PopAsync(animated: true)));
    }
    protected override void OnAppearing() => this.Dispatcher.Dispatch(() =>
    {
        this.GetCommentInfoCommand.Execute(null);
        this.PageIndex = 1;
        this.GetCommentsListCommand.Execute(null);
    });
    public virtual void SetNavigationQuery(IDictionary<string, object> queries) => this.RecipeId = (int)queries["RecipeId"];
    protected override void OnDisappearing() => base.OnDisappearing();
}