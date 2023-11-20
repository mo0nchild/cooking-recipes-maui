namespace MauiLabs.View.Services.ApiModels.Commons.RecipeModels
{
    /// <summary>
    /// Информация об издателе рецепта
    /// </summary>
    public partial class PublisherInfoModel : object
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public required string Name { get; set; } = default!;

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public required string Surname { get; set; } = default!;

        /// <summary>
        /// Изображение профиля пользователя
        /// </summary>
        public byte[] Image { get; set; } = default;
    }
}
