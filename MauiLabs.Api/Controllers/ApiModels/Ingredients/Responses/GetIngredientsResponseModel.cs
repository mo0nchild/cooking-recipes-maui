using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.IngredientItemRequests.GetIngredientItems;
using MauiLabs.Api.Services.Requests.IngredientItemRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Responses
{
    /// <summary>
    /// Список всех игредиентов
    /// </summary>
    public partial class GetIngredientsResponseModel : IMappingTarget<IngredientItemsCollection>
    {
        /// <summary>
        /// Данные ингредиентов в форме списка
        /// </summary>
        public required List<IngredientItemInfo> IngredientItems { get; set; } = new();
        
        /// <summary>
        /// Количество всех ингредиентов
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
