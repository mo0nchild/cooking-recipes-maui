using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        public ICommand RegistrationCommand { get; protected set; } = default!;
        public ICommand CancelCommand { get; protected set; } = default!;

        public event PropertyChangedEventHandler PropertyChanged = default;
        public RegistrationViewModel(IUserAuthorization authorization) : base()
        {
            this.userAuthorization = authorization;
            this.RegistrationCommand = new Command(this.RegistrationCommandHandler);
            this.CancelCommand = new Command(this.CancelCommandHandler);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            this.cancellationSource.Cancel();
            this.cancellationSource = new CancellationTokenSource();

            this.IsLoading = default;
        });
        protected virtual async void RegistrationCommandHandler(object sender)
        {
            return;

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
            set { this.registrationRequestModel.Image = value; this.OnPropertyChanged(); }
            get => this.registrationRequestModel.Image;
        }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public Dictionary<string, bool> ValidationState { get; set; } = new();

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
