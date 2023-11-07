using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientItem;

namespace MauiLabs.Api.Controllers.ApiModels.Ingredients.Requests
{
    /// <summary>
    /// Данные для удаления ингредиента
    /// </summary>
    public partial class DeleteIngredientRequestModel : IMappingTarget<DeleteIngredientItemCommand>
    {
        /// <summary>
        /// Название ингредиента
        /// </summary>
        public required string Name { get; set; } = default!;
    }
}
