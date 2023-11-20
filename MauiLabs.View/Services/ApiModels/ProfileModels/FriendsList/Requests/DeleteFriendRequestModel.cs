using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.FriendsList.Requests
{
    /// <summary>
    /// Данные для удаления записи о дружбе
    /// </summary>
    public partial class DeleteFriendRequestModel : object
    {
        /// <summary>
        /// Идентификатор записи о дружбе
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор записи")]
        public required int RecordId { get; set; } = default!;
    }
}
