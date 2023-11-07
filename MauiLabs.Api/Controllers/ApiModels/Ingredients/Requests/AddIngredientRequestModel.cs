using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Requests
{
    /// <summary>
    /// Данные для добавления ингредиента
    /// </summary>
    public partial class AddIngredientRequestModel : IMappingTarget<AddIngredientItemCommand>
    {
        /// <summary>
        /// Название ингредиента
        /// </summary>
        public required string IngredientName { get; set; } = default!;
        
        /// <summary>
        /// Единицы измерения
        /// </summary>
        public required string UnitName { get; set; } = default!;
    }
}
