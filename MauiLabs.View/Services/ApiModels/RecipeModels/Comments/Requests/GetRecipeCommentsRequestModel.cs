using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests
{
    public enum CommentSortingType : sbyte { ByDate, ByRating }

    /// <summary>
    /// Данные для получения списка комментарий рецепта
    /// </summary>
    public partial class GetRecipeCommentsRequestModel : object
    {
        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;

        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество пропущенных записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Skip] не может быть отрицательным")]
        public required int Skip { get; set; } = default!;

        /// <summary>
        /// Количество обработанных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество обработанных записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Take] не может быть отрицательным")]
        public required int Take { get; set; } = default!;

        /// <summary>
        /// Правила сортировки найденных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать правила сортировки найденных записей")]
        [EnumDataType(typeof(CommentSortingType), ErrorMessage = "Неверное значение правила сортировки")]
        public CommentSortingType SortingType { get; set; } = default!;
    }
}
