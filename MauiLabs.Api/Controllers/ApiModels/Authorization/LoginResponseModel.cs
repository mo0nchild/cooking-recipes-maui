namespace MauiLabs.Api.Controllers.ApiModels.Authorization
{
    /// <summary>
    /// Результат запроса авторизации пользователя
    /// </summary>
    public partial class LoginResponseModel : object
    {
        /// <summary>
        /// Jwt токен для аутентификация профиля
        /// </summary>
        public string JwtToken { get; set; } = default!; 
    }
}
