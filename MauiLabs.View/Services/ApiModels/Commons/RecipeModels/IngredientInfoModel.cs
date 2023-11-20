namespace MauiLabs.View.Services.ApiModels.Commons.RecipeModels
{
    /// <summary>
    /// Информация об ингредиенте
    /// </summary>
    public partial class IngredientInfoModel : object
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
