using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Commons.RecipeModels;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.CookingRecipe.Requests
{
    /// <summary>
    /// Данные для редактирования кулинарного рецепта
    /// </summary>
    public partial class EditRecipeRequestModel : IMappingTarget<EditCookingRecipeCommand>, IValidatableObject
    {
        /// <summary>
        /// Идентификатор кулинарного рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор кулинарного рецепта")]
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Название кулинарного рецепта
        /// </summary>
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина название рецепта в диапазоне от 3 до 50 символов")]
        [Required(ErrorMessage = "Необходимо указать название кулинарного рецепта")]
        public required string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание кулинарного рецепта
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// Изображение рецепта
        /// </summary>
        public byte[]? Image { get; set; } = default!;

        /// <summary>
        /// Категория рецепта
        /// </summary>
        [MaxLength(50, ErrorMessage = "Длина названия категории рецепта до 50 символов")]
        public string? Category { get; set; } = default!;

        /// <summary>
        /// Список ингредиентов
        /// </summary>
        public Dictionary<string, IngredientUnitModel> Ingredients { get; set; } = new();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResult = new List<ValidationResult>();
            foreach (var item in this.Ingredients)
            {
                var results = new List<ValidationResult>();
                if (Validator.TryValidateObject(item.Value, new ValidationContext(item.Value), results, true))
                {
                    validationResult.AddRange(results);
                }
                if (item.Key.Length > 50 || item.Key.Length < 3)
                    validationResult.Add(new ValidationResult("Длина названия ингредиента в диапазоне от 3 до 50 символов"));
            }
            return validationResult;
        }
    }
}
