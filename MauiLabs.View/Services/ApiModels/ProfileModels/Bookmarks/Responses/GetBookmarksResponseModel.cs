﻿using MauiLabs.View.Services.ApiModels.Commons.ProfileModels;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Responses
{
    /// <summary>
    /// Список заметок рецептов пользователя
    /// </summary>
    public partial class GetBookmarksResponseModel : object
    {
        /// <summary>
        /// Данные о заметках в форме списка
        /// </summary>
        public required List<BookmarkInfoModel> Bookmarks { get; set; } = new();

        /// <summary>
        /// Общее количество заметок рецептов
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
