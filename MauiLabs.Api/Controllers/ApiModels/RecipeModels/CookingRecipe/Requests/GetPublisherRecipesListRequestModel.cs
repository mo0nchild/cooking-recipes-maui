using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для получения рецептов опубликованных пользователем
    /// </summary>
    public partial class GetPublisherRecipesListRequestModel : IMappingTarget<GetPublishedRecipeListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int PublisherId { get; set; } = default!;
    }
}
