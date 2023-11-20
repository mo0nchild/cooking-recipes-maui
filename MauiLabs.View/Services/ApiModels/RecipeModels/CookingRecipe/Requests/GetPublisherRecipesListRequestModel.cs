using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для получения рецептов опубликованных пользователем
    /// </summary>
    public partial class GetPublisherRecipesListRequestModel : object
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int PublisherId { get; set; } = default!;
    }
}
