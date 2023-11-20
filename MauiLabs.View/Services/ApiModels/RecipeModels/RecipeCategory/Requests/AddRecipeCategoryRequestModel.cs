using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.RecipeCategory.Requests
{
    /// <summary>
    /// Данные для добавления категории рецептов
    /// </summary>
    public partial class AddRecipeCategoryRequestModel : object
    {
        /// <summary>
        /// Название категории
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина названия в диапазоне от 3 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать название категории")]
        public required string Name { get; set; } = default!;
    }
}
