using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.Commons.ViewModels.ProfilesViewModels
{
    public partial class AuthorizationViewModel : INotifyPropertyChanged
    {
        protected internal readonly IUserAuthorization userAuthorization = default!;
        protected internal CancellationTokenSource cancellationSource = new();
        public ICommand AuthorizateCommand { get; protected set; } = default!;
        public ICommand CancelCommand { get; protected set; } = default!;

        public event PropertyChangedEventHandler PropertyChanged = default;
        public AuthorizationViewModel(IUserAuthorization authorization) : base()
        {
            this.userAuthorization = authorization;
            this.AuthorizateCommand = new Command(AuthorizateCommandHandler);
            this.CancelCommand = new Command(CancelCommandHandler);
        }
        protected virtual async void CancelCommandHandler(object sender) => await Task.Run(() =>
        {
            this.cancellationSource.Cancel();
            this.cancellationSource = new CancellationTokenSource();

            this.IsLoading = default;
        });
        protected virtual async void AuthorizateCommandHandler(object sender)
        {
            if (!this.IsLoginValid || !this.IsPasswordValid) return;
            this.IsLoading = true;
            LoginResponseModel requestResult = default!;
            try {
                requestResult = await userAuthorization.AuthorizeUser(loginRequestModel, cancellationSource.Token);
                await Application.Current.MainPage.DisplayAlert("Успех", requestResult.JwtToken, "Назад");
            }
            catch (InvalidOperationException errorInfo) { await Console.Out.WriteLineAsync(errorInfo.Message); }
            catch (ViewServiceException errorInfo)
            {
                await Application.Current.MainPage.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
            }
            catch (TaskCanceledException errorInfo) { await Console.Out.WriteLineAsync(errorInfo.Message); }
            this.IsLoading = false;
        }
        private protected bool isLoginValid = default!;
        public bool IsLoginValid { get => this.isLoginValid; set { this.isLoginValid = value; OnPropertyChanged(); } }

        private protected bool isPasswordValid = default!;
        public bool IsPasswordValid { get => this.isPasswordValid; set { this.isPasswordValid = value; OnPropertyChanged(); } }

        private protected LoginRequestModel loginRequestModel = new();
        public string UserLogin
        {
            get => this.loginRequestModel.Login;
            set { this.loginRequestModel.Login = value; OnPropertyChanged(); }
        }
        public string UserPassword
        {
            get => this.loginRequestModel.Password;
            set { this.loginRequestModel.Password = value; OnPropertyChanged(); }
        }

        private protected bool isLoading = default;
        public bool IsLoading { get => this.isLoading; set { this.isLoading = value; OnPropertyChanged(); } }

        public virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
