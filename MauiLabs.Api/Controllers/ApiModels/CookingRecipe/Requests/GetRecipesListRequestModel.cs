using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для получения списка кулинарный рецептов
    /// </summary>
    public partial class GetRecipesListRequestModel : IMappingTarget<GetCookingRecipesListRequest>
    {
        /// <summary>
        /// Количество пропускаемых записей
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
        /// Фильтр по состоянию рецепта
        /// </summary>
        public bool? Confirmed { get; set; } = default!;

        /// <summary>
        /// Порядок сортировки списка
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать правила сортировки найденных записей")]
        [EnumDataType(typeof(RecipeSortingType), ErrorMessage = "Неверное значение правила сортировки")]
        public required RecipeSortingType SortingType { get; set; } = default!;

        /// <summary>
        /// Название категории рецепта
        /// </summary>
        [MaxLength(50, ErrorMessage = "Длина названия категории рецепта до 50 символов")]
        public string? Category { get; set; } = default!;
        
        /// <summary>
        /// Фильтр по названию рецепта
        /// </summary>
        public string? TextFilter { get; set; } = default;
    }
}
