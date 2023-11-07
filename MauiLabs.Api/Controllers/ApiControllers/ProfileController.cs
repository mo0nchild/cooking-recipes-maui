using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks;
using MauiLabs.Api.Controllers.ApiModels.Profile.Requests;
using MauiLabs.Api.Controllers.ApiModels.Profile.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo;
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
        /// Изменение данных профиля пользователя
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Статус изменения профиля</returns>
        [Route("edit"), HttpPut]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditProfileHandler([FromBody] EditProfileRequestModel request)
        {
            var mappedRequest = this.mapper.Map<EditProfileCommand>(request);
            mappedRequest.Id = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <returns>Статус удаления профиля</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProfileHandler()
        {
            try { await this.mediator.Send(new DeleteProfileCommand() { Id = this.ProfileId }); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
        /// <summary>
        /// Получение информации о профиле пользователя при помощи токена
        /// </summary>
        /// <returns>Информация о профиле пользователя</returns>
        [Route("getbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetProfileInfoResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileInfoByTokenHandler()
        {
            var requestResult = await this.mediator.Send(new GetProfileInfoRequest() { Id = this.ProfileId });
            if (requestResult == null)
            {
                return this.Problem("Профиль не найден", statusCode: (int)StatusCodes.Status400BadRequest);
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
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileInfoHandler([FromQuery] GetProfileInfoRequestModel request)
        {
            var requestResult = await this.mediator.Send(this.mapper.Map<GetProfileInfoRequest>(request));
            if (requestResult == null)
            {
                return this.Problem("Профиль не найден", statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetProfileInfoResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение списка пользователей системы
        /// </summary>
        /// <param name="request">Настройки поиска пользователей</param>
        /// <returns>Информация о профиле пользователя</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetProfileInfoResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfilesListHandler([FromQuery] GetProfilesListRequestModel request)
        {
            var requestResult = await this.mediator.Send(this.mapper.Map<GetProfileInfoRequest>(request));
            if (requestResult == null)
            {
                return this.Problem("Профиль не найден", statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetProfileInfoResponseModel>(requestResult));
        }
    }
}
