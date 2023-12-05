using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.ProfilesViewModels
{
    public partial class RegistrationViewModel : INotifyPropertyChanged
    {
        protected internal readonly IUserAuthorization userAuthorization = default!;
        protected internal CancellationTokenSource cancellationSource = new();
        public virtual double ImageSize { get => 112; }

        public static readonly string DefaultProfileImage = $"MauiLabs.View.Resources.Images.Profile.defaultprofile.png";

        public ICommand RegistrationCommand { get; protected set; } = default!;
        public ICommand ImagePickerCommand { get; protected set; } = default!;
        public ICommand ImageClearCommand { get; protected set; } = default!;
        public ICommand CancelCommand { get; protected set; } = default!;

        public event EventHandler<string> DisplayAlert = delegate { };
        public event PropertyChangedEventHandler PropertyChanged = default;
        public RegistrationViewModel(IUserAuthorization authorization) : base()
        {
            this.userAuthorization = authorization;
            this.RegistrationCommand = new Command(this.RegistrationCommandHandler);
            this.CancelCommand = new Command(this.CancelCommandHandler);

            this.ImagePickerCommand = new Command(this.ImagePickerCommandHandler);
            this.ImageClearCommand = new Command(async () => 
            { 
                this.UserImage = null;
                this.PreviewImage = await this.FileToByteArray(DefaultProfileImage);
            });
            this.ImageClearCommand.Execute(this);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            this.cancellationSource.Cancel();
            this.cancellationSource = new CancellationTokenSource();

            this.IsLoading = default;
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
            var fileFilter = (FileResult result, string extension) => 
            {
                return result.FileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
            };
            var pickerOption = new PickOptions() { FileTypes = FilePickerFileType.Images, PickerTitle = "Выберите изображение" };
            try {
                var pickerResult = await FilePicker.Default.PickAsync(pickerOption);
                if (pickerResult != null && (fileFilter(pickerResult, "jpg") || fileFilter(pickerResult, "png")))
                {
                    using var stream = await pickerResult.OpenReadAsync();
                    using var image = SixLabors.ImageSharp.Image.Load(stream);
                    image.Mutate(prop => prop.Resize((int)this.ImageSize, (int)this.ImageSize, false));

                    using var outputStream = new MemoryStream();
                    image.Save(outputStream, new PngEncoder());
                    this.UserImage = this.PreviewImage = outputStream.ToArray();
                }
            }
            catch (Exception errorInfo) { this.DisplayAlert.Invoke(this, errorInfo.Message); }
        }

        protected virtual async void RegistrationCommandHandler(object sender)
        {
            if (this.ValidationState.Where(item => !item.Value).Count() > 0) return;
            this.IsLoading = true;
            LoginResponseModel requestResult = default!;
            try {
                requestResult = await this.userAuthorization.RegistrationUser(registrationRequestModel, cancellationSource.Token);
                await UserManager.AuthorizeUser(requestResult);
                await Shell.Current.GoToAsync("//recipes", true);
            }
            catch (ViewServiceException errorInfo)
            {
                await Application.Current.MainPage.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
            }
            catch (Exception errorInfo) { await Console.Out.WriteLineAsync(errorInfo.Message); }
            this.IsLoading = false;
        }
        private protected RegistrationRequestModel registrationRequestModel = new();
        public string UserName 
        {
            set { this.registrationRequestModel.Name = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Name;
        }
        public string UserSurname 
        {
            set { this.registrationRequestModel.Surname = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Surname;
        }
        public string UserEmail 
        {
            set { this.registrationRequestModel.Email = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Email; 
        }

        public string UserLogin
        {
            set { this.registrationRequestModel.Login = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Login;
        }
        public string UserPassword 
        {
            set { this.registrationRequestModel.Password = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Password;
        }
        public byte[] UserImage 
        {
            set {  this.PreviewImage = this.registrationRequestModel.Image = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Image;
        }

        protected internal byte[] previewImage = new byte[0];
        public byte[] PreviewImage { get => this.previewImage; set { this.previewImage = value; OnPropertyChanged(); } }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public Dictionary<string, bool> ValidationState { get; set; } = new();

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
