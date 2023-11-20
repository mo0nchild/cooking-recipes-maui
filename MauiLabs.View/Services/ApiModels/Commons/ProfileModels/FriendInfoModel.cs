namespace MauiLabs.View.Services.ApiModels.Commons.ProfileModels
{
    /// <summary>
    /// Данные о друге пользователя
    /// </summary>
    public partial class FriendInfoModel : object
    {
        /// <summary>
        /// Идентификатор друга
        /// </summary>
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Время регистрации дружбы
        /// </summary>
        public required DateTime DateTime { get; set; } = default!;

        /// <summary>
        /// Данные профиля друга
        /// </summary>
        public required ProfileInfoModel Profile { get; set; } = default!;
    }
}
