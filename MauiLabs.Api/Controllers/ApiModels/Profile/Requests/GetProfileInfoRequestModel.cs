using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int Id { get; set; } = default!;
    }
}
