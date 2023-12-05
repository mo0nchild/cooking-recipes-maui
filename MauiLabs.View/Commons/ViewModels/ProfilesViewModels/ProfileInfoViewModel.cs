using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.ProfilesViewModels
{
    public partial class ProfileInfoViewModel : INotifyPropertyChanged
    {
        protected internal readonly IUserProfile profileService = default!;
        protected internal CancellationTokenSource cancellationSource = new();
        public virtual double ImageSize { get => 96; }

        public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";

        public ICommand CancelCommand { get; protected set; } = default!;
        public ICommand GetProfileCommand { get; protected set; } = default!;
        public ICommand UpdateProfileCommand { get; protected set; } = default!;

        public ICommand ImagePickerCommand { get; protected set; } = default!;
        public ICommand ImageClearCommand { get; protected set; } = default!;

        public event EventHandler<string> DisplayAlert = delegate { };
        public event EventHandler<string> DisplayInfo = delegate { };
        public event EventHandler ReloadView = delegate { };

        public event PropertyChangedEventHandler PropertyChanged;
        public ProfileInfoViewModel(IUserProfile profileService) : base()
        {
            this.profileService = profileService;
            this.GetProfileCommand = new Command(() => this.LaunchСancelableTask(() => this.GetProfileCommandHandler()));
            this.UpdateProfileCommand = new Command(() =>
            {
                if (!this.ProfileLoaded) { this.DisplayAlert.Invoke(this, "Необходимо загрузить профиль"); return; }
                if (this.ValidationState.Where(item => !item.Value).Count() > 0)
                {
                    this.DisplayAlert.Invoke(this, "Неверно заполнены поля"); return;
                }
                this.LaunchСancelableTask(() => this.UpdateProfileCommandHander());
            });
            this.CancelCommand = new Command(this.CancelCommandHandler);

            this.ImagePickerCommand = new Command(this.ImagePickerCommandHandler);
            this.ImageClearCommand = new Command(async () =>
            {
                if (!this.ProfileLoaded) { this.DisplayAlert.Invoke(this, "Необходимо перезагрузить профиль"); return; }
                this.UserImage = null;
                this.PreviewImage = await this.FileToByteArray(DefaultProfileImage);
            });
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            this.cancellationSource.Cancel(); 
            this.cancellationSource = new CancellationTokenSource();

            (this.IsLoading, this.ProfileLoaded) = (default, default);
        });
        protected async void LaunchСancelableTask(Func<Task> cancelableTask) => await Task.Run(async () =>
        {
            this.IsLoading = true; await cancelableTask.Invoke();
            this.IsLoading = false;
        });

        private Task<byte[]> FileToByteArray(string filename)
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                using var binaryReader = new BinaryReader(fileStream);
                return Task.FromResult(binaryReader.ReadBytes((int)fileStream.Length));
            }
        }
        protected virtual async void ImagePickerCommandHandler(object sender)
        {
            if(!this.ProfileLoaded) { this.DisplayAlert.Invoke(this, "Необходимо перезагрузить профиль"); return; }
            var fileFilter = (FileResult result, string extension) =>
            {
                return result.FileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
            };
            var pickerOption = new PickOptions() { FileTypes = FilePickerFileType.Images, PickerTitle = "Выберите изображение" };
            try
            {
                var pickerResult = await FilePicker.Default.PickAsync(pickerOption);
                if (pickerResult != null && (fileFilter(pickerResult, "jpg") || fileFilter(pickerResult, "png")))
                {
                    using var stream = await pickerResult.OpenReadAsync();
                    using var image = SixLabors.ImageSharp.Image.Load(stream);
                    image.Mutate(prop => prop.Resize((int)this.ImageSize, (int)this.ImageSize, false));

                    using var outputStream = new MemoryStream();
                    image.Save(outputStream, new PngEncoder());
                    this.UserImage = outputStream.ToArray();
                }
            }
            catch (Exception errorInfo) { this.DisplayAlert.Invoke(this, errorInfo.Message); }
        }

        protected virtual async Task UpdateProfileCommandHander() => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.profileService.EditProfileInfo(new RequestInfo<EditProfileRequestModel>()
            {
                RequestModel = new EditProfileRequestModel()
                {
                    Name = this.UserName, Surname = this.UserSurname,
                    Email = this.UserEmail, Image = this.UserImage,
                },
                ProfileToken = token, CancelToken = this.cancellationSource.Token,
            });
            this.DisplayInfo.Invoke(this, "Данные успешно обновлены");
            this.ReloadView.Invoke(this, new EventArgs());
        });
        protected virtual async Task GetProfileCommandHandler() => await UserManager.SendRequest(async (token) =>
        {
            var requestResult = await this.profileService.GetProfileInfoByToken(token, this.cancellationSource.Token);
            if (requestResult.Image.Length != 0)
            {
                this.PreviewImage = await this.FileToByteArray(DefaultProfileImage);
                this.UserImage = requestResult.Image;
            }
            else this.PreviewImage = this.PreviewImage = requestResult.Image;

            (this.UserName, this.UserSurname) = (requestResult.Name, requestResult.Surname);
            (this.UserEmail, this.ReferenceLink) = (requestResult.Email, requestResult.ReferenceLink);

            if (!this.ProfileLoaded) this.ProfileLoaded = true;
            this.ReloadView.Invoke(this, new EventArgs());
        }, async (errorInfo) =>
        {
            this.UserName = this.UserSurname = this.UserEmail = "Не загружено";
            (this.ReferenceLink, this.UserImage) = (string.Empty, null);

            this.PreviewImage = await this.FileToByteArray(DefaultProfileImage);
            this.DisplayAlert.Invoke(this, "Данные не загружены, для редактирования обновить");
        });
        private protected GetProfileInfoResponseModel profileInfoModel = new() 
        {
            Id = -1, Name = "Не загружено", Surname = "Не загружено",
            Email = "Не загружено", ReferenceLink = string.Empty, 
        };
        public string UserName
        {
            set { this.profileInfoModel.Name = value; OnPropertyChanged(); }
            get => this.profileInfoModel.Name;
        }
        public string UserSurname
        {
            set { this.profileInfoModel.Surname = value; OnPropertyChanged(); }
            get => this.profileInfoModel.Surname;
        }
        public string UserEmail
        {
            set { this.profileInfoModel.Email = value; OnPropertyChanged(); }
            get => this.profileInfoModel.Email;
        }
        public byte[] UserImage
        {
            set { this.profileInfoModel.Image = value; this.OnPropertyChanged(); }
            get => this.profileInfoModel.Image;
        }
        public string ReferenceLink 
        {
            set { this.profileInfoModel.ReferenceLink = value; this.OnPropertyChanged(); }
            get => this.profileInfoModel.ReferenceLink; 
        }

        protected internal byte[] previewImage = new byte[0];
        public byte[] PreviewImage { get => this.previewImage; set { this.previewImage = value; OnPropertyChanged(); } }


        private protected bool profileLoaded = default;
        public bool ProfileLoaded { get => this.profileLoaded; set { this.profileLoaded = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public Dictionary<string, bool> ValidationState { get; set; } = new();
        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
