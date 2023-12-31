﻿using MauiLabs.View.Services.ApiModels.Commons.RecipeModels;

namespace MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Responses
{
    /// <summary>
    /// Список комментарий о рецепте
    /// </summary>
    public partial class GetCommentsResponseModel : object
    {
        /// <summary>
        /// Данные о комментариях в форме списка
        /// </summary>
        public required List<CommentInfoModel> Comments { get; set; } = new();

        /// <summary>
        /// Общее количество комментариев
        /// </summary>
        public required int AllCount { get; set; } = default!;
    }
}
