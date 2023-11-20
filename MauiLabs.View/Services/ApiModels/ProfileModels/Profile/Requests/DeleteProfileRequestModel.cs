using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests
{
    /// <summary>
    /// Данные для удаления профиля пользователя
    /// </summary>
    public partial class DeleteProfileRequestModel : IMappingTarget<DeleteProfileCommand>
    {
        /// <summary>
        /// Идентификатор удаляемого профиля 
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор удаляемого профиля")]
        public required int Id { get; set; } = default!;
    }
}
