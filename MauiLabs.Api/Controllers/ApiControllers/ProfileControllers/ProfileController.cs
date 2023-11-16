using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks;
using MauiLabs.Api.Controllers.ApiModels.Profile.Requests;
using MauiLabs.Api.Controllers.ApiModels.Profile.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword;
using MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/profile"), ApiController]
    public partial class ProfileController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// Изменение данных профиля пользователя при помощи токена
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Статус изменения профиля</returns>
        [Route("editbytoken"), HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditProfileByTokenHandler([FromBody] EditProfileByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<EditProfileCommand>(request);
            mappedRequest.Id = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
        /// <summary>
        /// Удаление профиля пользователя при помощи токена
        /// </summary>
        /// <returns>Статус удаления профиля</returns>
        [Route("deletebytoken"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProfileByTokenHandler()
        {
            try { await this.mediator.Send(new DeleteProfileCommand() { Id = this.ProfileId }); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно удалены");
        }
        /// <summary>
        /// Получение информации о профиле пользователя при помощи токена
        /// </summary>
        /// <returns>Информация о профиле пользователя</returns>
        [Route("getbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetProfileInfoResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileInfoByTokenHandler()
        {
            ProfileInfo? requestResult = default!;
            try {
                requestResult = await this.mediator.Send(new GetProfileInfoRequest() { Id = this.ProfileId });
                if (requestResult == null) throw new ValidationException("Профиль не найден");
            }
            catch (ValidationException errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetProfileInfoResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение информации о профиле пользователя
        /// </summary>
        /// <param name="request">Идентификатор пользователя</param>
        /// <returns>Информация о профиле пользователя</returns>
        [Route("get"), HttpGet]
        [ProducesResponseType(typeof(GetProfileInfoResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileInfoHandler([FromQuery] GetProfileInfoRequestModel request)
        {
            ProfileInfo? requestResult = default!;
            try {
                requestResult = await this.mediator.Send(this.mapper.Map<GetProfileInfoRequest>(request));
                if (requestResult == null) throw new ValidationException("Профиль не найден");
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetProfileInfoResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение списка пользователей системы
        /// </summary>
        /// <param name="request">Настройки поиска пользователей</param>
        /// <returns>Список профилей пользователей</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetProfilesListRequestModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfilesListHandler([FromQuery] GetProfilesListRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetAllProfilesRequest>(request);
            try { return this.Ok(this.mapper.Map<GetProfilesListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// [Admin] Изменение данных профиля пользователя
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Статус изменения профиля</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("edit"), HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditProfileHandler([FromBody] EditProfileRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<EditProfileCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
        /// <summary>
        /// [Admin] Удаление профиля пользователя
        /// </summary>
        /// <param name="request">Идентификатор пользователя</param>
        /// <returns>Статус удаления профиля</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProfileHandler([FromQuery] DeleteProfileRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteProfileCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно удалены");
        }
        /// <summary>
        /// Изменение пароля профиля пользователя при помощи токена
        /// </summary>
        /// <param name="request">Новый и старый пароль</param>
        /// <returns>Статус изменения пароля</returns>
        [Route("editbytoken/password"), HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePasswordByTokenHandler([FromBody] ChangePasswordByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<ChangePasswordCommand>(request);
            mappedRequest.Id = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Пароль профиля успешно изменен");
        }
        /// <summary>
        /// [Admin] Изменение пароля профиля пользователя
        /// </summary>
        /// <param name="request">Новый и старый пароль</param>
        /// <returns>Статус изменения пароля</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("edit/password"), HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangePasswordHandler([FromBody] ChangePasswordRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<ChangePasswordCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Пароль профиля успешно изменен");
        }
    }
}
