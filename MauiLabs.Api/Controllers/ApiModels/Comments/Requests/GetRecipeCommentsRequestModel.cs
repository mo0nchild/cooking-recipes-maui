using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.Comments.Requests
{
    /// <summary>
    /// Данные для получения списка комментарий рецепта
    /// </summary>
    public partial class GetRecipeCommentsRequestModel : IMappingTarget<GetRecipeCommentsListRequest>
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
        public required int Skip { get; set; } = default!;

        /// <summary>
        /// Количество обработанных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество обработанных записей")]
        public required int Take { get; set; } = default!;

        /// <summary>
        /// Правила сортировки найденных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать правила сортировки найденных записей")]
        public CommentSortingType SortingType { get; set; } = default!;
    }
}
