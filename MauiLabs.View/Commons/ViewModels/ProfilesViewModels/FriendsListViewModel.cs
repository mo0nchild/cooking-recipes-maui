using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;
using MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using SixLabors.ImageSharp.Advanced;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.ProfilesViewModels
{
    public partial class FriendsListViewModel : INotifyPropertyChanged
    {
        protected internal CancellationTokenSource cancellationSource = new();
        protected internal readonly IFriendsList friendsService = default!;

        public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";

        public ICommand GetFriendsListCommand { get; protected set; } = default!;
        public ICommand AddFriendCommand { get; protected set; } = default!;
        public ICommand DeleteFriendCommand { get; protected set; } = default!;
        public ICommand CancelCommand { get; protected set; } = default!;

        public event EventHandler<string> DisplayInfo = delegate { };
        public event EventHandler FriendsReload = delegate { };
        public event Func<string, Task<bool>> CheckСonfirm = (_) => Task.FromResult(false);

        public event PropertyChangedEventHandler PropertyChanged;
        public FriendsListViewModel(IFriendsList friendsService) : base()
        {
            this.friendsService = friendsService;
            this.AddFriendCommand = new Command(() =>
            {
                if (this.ReferenceLink.Length >= 5) this.LaunchСancelableTask(() => this.AddFriendCommandHandler());
            });
            this.DeleteFriendCommand = new Command<int>(async (id) =>
            {
                if(await this.CheckСonfirm.Invoke("Удалить данный профиль?"))
                {
                    this.LaunchСancelableTask(() => this.DeleteFriendCommandHandler(id));
                }
            });
            this.GetFriendsListCommand = new Command(() =>
            {
                this.LaunchСancelableTask(() => this.GetFriendsListCommandHandler());
            });
            this.CancelCommand = new Command(this.CancelCommandHandler);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            if (this.isLoading == false) return;
            this.cancellationSource.Cancel();

            this.cancellationSource = new CancellationTokenSource();
            (this.IsLoading, this.FriendsLoaded, this.AllCount) = (default, default, 0);
        });
        protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
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
        protected virtual async Task GetFriendsListCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.friendsService.GetFriendsList(token, this.cancellationSource.Token);
            foreach (var friendRecord in requestResult.Friends)
            {
                friendRecord.Profile.Image = friendRecord.Profile.Image.Length != 0
                    ? friendRecord.Profile.Image : this.FileToByteArray(DefaultProfileImage);
            }
            this.FriendsList = new(requestResult.Friends);
            this.AllCount = requestResult.AllCount;

            this.FriendsReload.Invoke(this, new EventArgs());
            if (!this.FriendsLoaded) this.FriendsLoaded = true;
        });
        protected virtual async Task AddFriendCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            await this.friendsService.AddFriend(new RequestInfo<AddFriendRequestModel>() 
            {
                RequestModel = new AddFriendRequestModel() { ReferenceLink = this.ReferenceLink },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            this.ReferenceLink = string.Empty;
            await this.GetFriendsListCommandHandler();
            this.DisplayInfo.Invoke(this, "Профиль друга добавлен");
        });
        protected virtual async Task DeleteFriendCommandHandler(int id) => await UserManager.SendRequest(async (token) =>
        {
            await this.friendsService.DeleteFriend(new RequestInfo<DeleteFriendRequestModel>()
            {
                RequestModel = new DeleteFriendRequestModel() { RecordId = id },
                CancelToken = this.cancellationSource.Token, ProfileToken = token,
            });
            await this.GetFriendsListCommandHandler();
            this.DisplayInfo.Invoke(this, "Профиль друга удален");
        });
        public ObservableCollection<FriendInfoModel> FriendsList { get; protected set; } = new();

        private protected string referenceLink = string.Empty;
        public string ReferenceLink { get => this.referenceLink; set { this.referenceLink = value; OnPropertyChanged(); } }
        
        private protected int allCount = default!;
        public int AllCount { get => this.allCount; set { this.IsEmpty = (this.allCount = value) <= 0; OnPropertyChanged(); } }

        private protected bool isEmpty = default!;
        public bool IsEmpty { get => this.isEmpty; set { this.isEmpty = value; OnPropertyChanged(); } }

        private protected bool friendsLoaded = default;
        public bool FriendsLoaded { get => this.friendsLoaded; set { this.friendsLoaded = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
