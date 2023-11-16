using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecommendsList.Requests
{
    /// <summary>
    /// Данные для рекомендации рецепта
    /// </summary>
    public partial class AddRecommendRequestModel : IMappingTarget<AddRecommendCommand>
    {
        /// <summary>
        /// Текст рекомендации рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать текст рекомендации")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Длина текста рекомендации в диапазоне от 10 до 200 символов")]
        public required string Text { get; set; } = default!;

        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор рецепта")]
        public required int RecipeId { get; set; } = default!;

        /// <summary>
        /// Идентификатор пользователя отправителя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя отправителя")]
        public required int FromUserId { get; set; } = default!;

        /// <summary>
        /// Идентификатор пользователя получателя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя получателя")]
        public required int ToUserId { get; set; } = default!;
    }
}
