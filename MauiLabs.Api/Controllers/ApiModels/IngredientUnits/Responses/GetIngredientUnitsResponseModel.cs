using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.IngredientRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Responses
{
    /// <summary>
    /// Список всех единиц измерения
    /// </summary>
    public partial class GetIngredientUnitsResponseModel : IMappingTarget<IngredientUnitsCollection>
    {
        /// <summary>
        /// Данные единиц измерения в форме списка
        /// </summary>
        public required List<IngredientUnitInfo> IngredientUnits { get; set; } = new();

        /// <summary>
        /// Количество всех единиц измерения
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
