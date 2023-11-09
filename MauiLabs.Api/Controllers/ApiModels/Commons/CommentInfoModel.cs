﻿using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;

namespace MauiLabs.Api.Controllers.ApiModels.Commons
{
    /// <summary>
    /// Информация о комментарии рецепта
    /// </summary>
    public partial class CommentInfoModel : IMappingTarget<CommentInfo>
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Текст комментария
        /// </summary>
        public required string Text { get; set; } = string.Empty;

        /// <summary>
        /// Значение рейтинга
        /// </summary>
        public required double Rating { get; set; } = default!;

        /// <summary>
        /// Дата и время добавления комментария
        /// </summary>
        public required DateTime PublicationTime { get; set; } = default;

        /// <summary>
        /// Идентификатор пользователя, который опубликовал комментарий
        /// </summary>
        public required int ProfileId { get; set; } = default!;

        /// <summary>
        /// Идентификатор рецепта
        /// </summary>
        public required int RecipeId { get; set; } = default!;
    }
}
