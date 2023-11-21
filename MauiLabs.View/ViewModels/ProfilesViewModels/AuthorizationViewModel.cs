using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiLabs.View.ViewModels.ProfilesViewModels
{
    public partial class AuthorizationViewModel : INotifyPropertyChanged
    {
        protected internal readonly IUserAuthorization userAuthorization = default!;
        public ICommand AuthorizateCommand { get; protected set; } = default!;

        public event PropertyChangedEventHandler PropertyChanged = default;
        public AuthorizationViewModel(IUserAuthorization authorization) : base()
        {
            this.userAuthorization = authorization;
            this.AuthorizateCommand = new Command(this.AuthorizateCommandHandler);
        }
        protected virtual async void AuthorizateCommandHandler(object sender)
        {
            this.IsVisible = false;
            LoginResponseModel requestResult = default!;
            try { 
                requestResult = await this.userAuthorization.AuthorizeUser(this.loginRequestModel);
                await Application.Current.MainPage.DisplayAlert("Успех", requestResult.JwtToken, "Назад");
            }
            catch(ViewServiceException errorInfo)
            {
                await Application.Current.MainPage.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
            }
            this.IsVisible = true;
            
            //await SecureStorage.Default.SetAsync("IsAdmin", requestResult.IsAdmin.ToString());
            //await SecureStorage.Default.SetAsync("ProfileId", requestResult.ProfileId.ToString());

            //await SecureStorage.Default.SetAsync("JwtToken", requestResult.JwtToken);
        }

        private LoginRequestModel loginRequestModel = new();
        public string UserLogin
        {
            get => this.loginRequestModel.Login;
            set {
                this.loginRequestModel.Login = value;
                this.OnPropertyChanged();
            }
        }
        public string UserPassword
        {
            get => this.loginRequestModel.Password;
            set {
                this.loginRequestModel.Password = value;
                this.OnPropertyChanged();
            }
        }

        private bool isVisible = true;
        public bool IsVisible {
            get => this.isVisible;
            set {
                this.isVisible = value;
                this.OnPropertyChanged();
            }
        } 

        public virtual void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
