﻿using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.CommentRequests.GetProfileCommentsList;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using System.ComponentModel.DataAnnotations;

namespace MauiLabs.Api.Controllers.ApiModels.RecipeModels.Comments.Requests
{
    /// <summary>
    /// Данные для получения списка комментарий пользователя
    /// </summary>
    public partial class GetProfileCommentsByIdRequestModel : GetProfileCommentsRequestModel, IMappingTarget<GetProfileCommentsListRequest>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать идентификатор пользователя")]
        public required int ProfileId { get; set; } = default!;
    }

    /// <summary>
    /// Данные для получения списка комментарий пользователя при помощи токена
    /// </summary>
    public partial class GetProfileCommentsRequestModel : IMappingTarget<GetProfileCommentsListRequest>
    {
        /// <summary>
        /// Количество пропущенных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество пропущенных записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Skip] не может быть отрицательным")]
        public required int Skip { get; set; } = default!;

        /// <summary>
        /// Количество обработанных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество обработанных записей записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Take] не может быть отрицательным")]
        public required int Take { get; set; } = default!;

        /// <summary>
        /// Правила сортировки найденных записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать правила сортировки найденных записей")]
        [EnumDataType(typeof(CommentSortingType), ErrorMessage = "Неверное значение правила сортировки")]
        public CommentSortingType SortingType { get; set; } = default!;
    }
}
