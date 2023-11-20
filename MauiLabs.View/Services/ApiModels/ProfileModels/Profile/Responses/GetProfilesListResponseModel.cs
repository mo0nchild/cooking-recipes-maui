using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses
{
    /// <summary>
    /// Список пользователей системы
    /// </summary>
    public partial class GetProfilesListResponseModel : object
    {
        /// <summary>
        /// Данные пользователей в форме списка
        /// </summary>
        public required List<ProfileInfoModel> Profiles { get; set; } = new();

        /// <summary>
        /// Количество всех записей о пользователях
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
