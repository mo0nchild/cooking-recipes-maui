﻿using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Dal.Entities;

namespace MauiLabs.Api.Controllers.ApiModels.Commons
{
    /// <summary>
    /// Информация о заметке кулинарного рецепта
    /// </summary>
    public partial class BookmarkInfoModel : IMappingTarget<BookmarkInfo>
    {
        /// <summary>
        /// Идентификатор заметки
        /// </summary>
        public required int Id { get; set; } = default!;

        /// <summary>
        /// Дата добавления заметки
        /// </summary>
        public required DateTime AddTime { get; set; } = default!;
        
        /// <summary>
        /// Данные рецепта, который добавлен как заметка
        /// </summary>
        public required CookingRecipeInfoModel Recipe { get; set; } = default!;
    }
}
