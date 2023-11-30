using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Commons
{
#nullable enable
    public static class UserManager : object
    {
        public static string AuthorizationRoute { get; private set; } = "//authorization";
        public static async Task AuthorizeUser(LoginResponseModel authorizationInfo)
        {
            await SecureStorage.Default.SetAsync("IsAdmin", authorizationInfo.IsAdmin.ToString());
            await SecureStorage.Default.SetAsync("ProfileId", authorizationInfo.ProfileId.ToString());

            await SecureStorage.Default.SetAsync("JwtToken", authorizationInfo.JwtToken);
        }
        public static async Task LogoutUser() => await Task.Run(async () => 
        {
            SecureStorage.Default.RemoveAll();
            await Application.Current!.Dispatcher.DispatchAsync(async () =>
            {
                await Shell.Current.Navigation.PopToRootAsync();
                await Shell.Current.GoToAsync(UserManager.AuthorizationRoute);
            });
        });
        public static async Task<bool?> IsAdmin() => bool.Parse(await SecureStorage.Default.GetAsync("IsAdmin"));
        public static async Task<int?> ProfileId() => int.Parse(await SecureStorage.Default.GetAsync("ProfileId"));
        public static async Task<string> JwtToken() => await SecureStorage.Default.GetAsync("JwtToken");

        public static async Task SendRequest(Func<string, Task> request, Func<Exception, Task>? cancelled = null)
        {
            try { await request.Invoke(await UserManager.JwtToken()); }
            catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType != HttpStatusCode.Unauthorized)
            {
                await Shell.Current.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
            }
            catch (Exception errorInfo) { await CatchRequestError(errorInfo, cancelled); }
        }
        public static async Task CatchRequestError(Exception errorInfo, Func<Exception, Task>? cancelled = null)
        {
            if (!(errorInfo is InvalidOperationException) && !(errorInfo is TaskCanceledException))
            {
                await Shell.Current.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад");
                await LogoutUser();
            }
            else if (cancelled != null) await cancelled.Invoke(errorInfo);
        }
    }
#nullable disable
}
