using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons.ProfileModels;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.Profile.Responses
{
    /// <summary>
    /// Список пользователей системы
    /// </summary>
    public partial class GetProfilesListResponseModel : IMappingTarget<ProfileCollection>
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
