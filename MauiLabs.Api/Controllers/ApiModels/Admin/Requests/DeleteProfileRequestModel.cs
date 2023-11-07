using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile;

namespace MauiLabs.Api.Controllers.ApiModels.Admin.Requests
{
    /// <summary>
    /// Данные для удаления профиля пользователя
    /// </summary>
    public partial class DeleteProfileRequestModel : IMappingTarget<DeleteProfileCommand>
    {
        /// <summary>
        /// Идентификатор удаляемого профиля 
        /// </summary>
        public required int Id { get; set; } = default!;
    }
}
