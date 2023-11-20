using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;

namespace MauiLabs.View.Services.ApiModels.Commons.RecipeModels
{
    /// <summary>
    /// Информация об издателе рецепта
    /// </summary>
    public partial class PublisherInfoModel : IMappingTarget<PublisherInfo>
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
