namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.Authorization.Responses
{
    /// <summary>
    /// Результат запроса авторизации пользователя
    /// </summary>
    public partial class LoginResponseModel : object
    {
        /// <summary>
        /// Jwt токен для аутентификация профиля
        /// </summary>
        public required string JwtToken { get; set; } = default!;

        /// <summary>
        /// Определение роли пользователя системы
        /// </summary>
        public required bool IsAdmin { get; set; } = default!;

        /// <summary>
        /// Идентификатор профиля пользователя
        /// </summary>
        public required int ProfileId { get; set; } = default!;
    }
}
