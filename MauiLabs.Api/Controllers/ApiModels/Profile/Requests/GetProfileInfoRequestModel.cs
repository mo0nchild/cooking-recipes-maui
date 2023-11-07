using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Requests
{
    /// <summary>
    /// Данные для получения информации профиля пользователя
    /// </summary>
    public partial class GetProfileInfoRequestModel : IMappingTarget<GetProfileInfoRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required int Id { get; set; } = default!;
    }
}
