﻿using System.ComponentModel.DataAnnotations;

namespace MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests
{
    /// <summary>
    /// Данные для получения списка пользователей
    /// </summary>
    public partial class GetProfilesListRequestModel : object
    {
        /// <summary>
        /// Фильтр по имени пользователя
        /// </summary>
        public string TextFilter { get; set; } = default!;

        /// <summary>
        /// Количество пропускаемых записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество пропускаемых записей записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Skip] не может быть отрицательным")]
        public required int Skip { get; set; } = default!;

        /// <summary>
        /// Количество загружаемый записей
        /// </summary>
        [Required(ErrorMessage = "Необходимо указать количество обработанных записей записей")]
        [Range(0, int.MaxValue, ErrorMessage = "Значение [Take] не может быть отрицательным")]
        public required int Take { get; set; } = default!;
    }
}
