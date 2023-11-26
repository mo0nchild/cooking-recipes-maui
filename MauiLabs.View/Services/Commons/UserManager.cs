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
        public static string LoginRoute { get; private set; } = "authorization";
        public static async Task AuthorizeUser(LoginResponseModel authorizationInfo) 
        {
            await SecureStorage.Default.SetAsync("IsAdmin", authorizationInfo.IsAdmin.ToString());
            await SecureStorage.Default.SetAsync("ProfileId", authorizationInfo.ProfileId.ToString());

            await SecureStorage.Default.SetAsync("JwtToken", authorizationInfo.JwtToken);
        }
        public static async Task LogoutUser() => await Task.Run(() => SecureStorage.Default.RemoveAll());

        public static async Task<bool?> IsAdmin() => bool.Parse(await SecureStorage.Default.GetAsync("IsAdmin"));
        public static async Task<int?> ProfileId() => int.Parse(await SecureStorage.Default.GetAsync("ProfileId"));
        public static async Task<string> JwtToken() => await SecureStorage.Default.GetAsync("JwtToken");

        public static async Task SendRequest(Func<string, Task> request)
        {
            try { await request.Invoke(await UserManager.JwtToken()); }
            catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType == HttpStatusCode.Unauthorized)
            {
                await UserManager.LogoutUser();
                await Shell.Current.GoToAsync("//authorization");
            }
            catch (ViewServiceException errorInfo) when (errorInfo.ExceptionType != HttpStatusCode.Unauthorized)
            {
                await Shell.Current.DisplayAlert("Произошла ошибка", errorInfo.Message, "Назад"); 
            }
            catch (Exception errorInfo) { await Console.Out.WriteLineAsync(errorInfo.Message); }
        }
    }
#nullable disable
}
