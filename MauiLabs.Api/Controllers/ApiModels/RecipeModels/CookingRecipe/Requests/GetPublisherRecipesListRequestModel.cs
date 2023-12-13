using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для получения рецептов опубликованных пользователем
    /// </summary>
    public partial class GetPublisherRecipesListByIdRequestModel : GetPublisherRecipesListRequestModel, 
        IMappingTarget<GetPublishedRecipeListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int PublisherId { get; set; } = default!;
    }

    /// <summary>
    /// Данные для получения рецептов опубликованных пользователем при помощи токена
    /// </summary>
    public partial class GetPublisherRecipesListRequestModel : IMappingTarget<GetPublishedRecipeListRequest>
    {
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
