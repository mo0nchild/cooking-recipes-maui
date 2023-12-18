using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests
{
    /// <summary>
    /// Данные для получения списка заметок рецептов
    /// </summary>
    public partial class GetBookmarksByIdRequestModel : GetBookmarksRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя, у которого ищутся заметки
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }

    /// <summary>
    /// Данные для получения списка заметок с использованием токена
    /// </summary>
    public partial class GetBookmarksRequestModel : object
    {
        /// <summary>
        /// Фильтр по названию рецепта
        /// </summary>
        public string TextFilter { get; set; } = default;

        /// <summary>
        /// Порядок сортировки по дате добавления
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать порядок сортировки по дате добавления")]
        public bool ReverseOrder { get; set; } = default!;

        /// <summary>
        /// Название категории рецепта
        /// </summary>
        [MaxLength(50, ErrorMessage = "Длина названия категории рецепта до 50 символов")]
        public string Category { get; set; } = default!;
    }
}
