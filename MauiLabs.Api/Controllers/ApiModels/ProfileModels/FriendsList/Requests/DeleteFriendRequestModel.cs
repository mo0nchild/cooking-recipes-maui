using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.ProfileModels.FriendsList.Requests
{
    /// <summary>
    /// Данные для удаления записи о дружбе
    /// </summary>
    public partial class DeleteFriendRequestModel : IMappingTarget<DeleteFriendCommand>
    {
        /// <summary>
        /// Идентификатор записи о дружбе
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор записи")]
        public required int RecordId { get; set; } = default!;
    }
}
