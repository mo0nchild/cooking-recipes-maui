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

        private static async Task DisplayAlertAsync(string message)
        {
            if (Application.Current != null && Application.Current.MainPage != null)
            {
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Произошла ошибка", message, "Назад");
                });
            }
            await Console.Out.WriteLineAsync(message);
        }
        public static async Task SendRequest(Func<string, Task> request, Func<Exception, Task>? cancelled = null)
        {
            try { await request.Invoke(await UserManager.JwtToken()); }
            catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType != HttpStatusCode.Unauthorized)
            {
                await UserManager.DisplayAlertAsync(errorInfo.Message);
            }
            catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType == HttpStatusCode.Unauthorized)
            {
                await UserManager.DisplayAlertAsync("Пользователь не авторизирован");
                await LogoutUser();
            }
            catch (TaskCanceledException errorInfo) when (errorInfo.InnerException is TimeoutException) 
            {
                await UserManager.DisplayAlertAsync("Время подключения к серверу истекло");
                await LogoutUser();
            }
            catch (Exception errorInfo) { await CatchRequestError(errorInfo, cancelled); }
        }
        public static async Task CatchRequestError(Exception errorInfo, Func<Exception, Task>? cancelled = null)
        {
            var errorType = errorInfo.GetType();
            /*
                Bad URL address (Domen):    [Desktop] = HttpRequestException, [Android] = WebException;
                Device turn of WIFI:        [Desktop] = HttpRequestException, [Android] = WebException,
            
                Bad URL address host:       404 Status code;
                Server OFF:                 503 Status code;

                Request cancelled:          [Desktop] = TaskCanceledException, [Android] = WebException;  
            */
            if (!(errorInfo is TaskCanceledException))
            {
                await UserManager.DisplayAlertAsync(errorInfo.Message);
                await LogoutUser();
            }
            else if (cancelled != null) await cancelled.Invoke(errorInfo);
        }
    }
#nullable disable
}
