using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Commons.RecipeModels
{
    /// <summary>
    /// Информация об ингредиенте
    /// </summary>
    public partial class IngredientInfoModel : IMappingTarget<IngredientInfo>
    {
        /// <summary>
        /// Необходимое количество ингредиента
        /// </summary>
        public required double Value { get; set; } = default!;

        /// <summary>
        /// Название ингредиента
        /// </summary>
        public required string Name { get; set; } = default!;

        /// <summary>
        /// Единицы измерения
        /// </summary>
        public required string Unit { get; set; } = default!;
    }
}
