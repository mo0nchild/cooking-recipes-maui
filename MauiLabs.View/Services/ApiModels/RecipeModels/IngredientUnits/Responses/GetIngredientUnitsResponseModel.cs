
namespace MauiLabs.View.Services.ApiModels.RecipeModels.IngredientUnits.Responses
{
    /// <summary>
    /// Список всех единиц измерения
    /// </summary>
    public partial class GetIngredientUnitsResponseModel : object
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
