using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Profile.Requests
{
    /// <summary>
    /// Данные для получения списка пользователей
    /// </summary>
    public partial class GetProfilesListRequestModel : IMappingTarget<GetAllProfilesRequest>
    {
        /// <summary>
        /// Фильтр по имени пользователя
        /// </summary>
        public string? TextFilter { get; set; } = default!;

        /// <summary>
        /// Количество пропускаемых записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество пропускаемых записей")]
        public required int Skip { get; set; } = default!;

        /// <summary>
        /// Количество загружаемый записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество загружаемый записей")]
        public required int Take { get; set; } = default!;
    }
}
