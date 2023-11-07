using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Admin.Requests;
using MauiLabs.Api.Controllers.ApiModels.Admin.Responses;
using MauiLabs.Api.Controllers.ApiModels.Profile.Requests;
using MauiLabs.Api.Controllers.ApiModels.Profile.Responses;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
    [Route("cookingrecipes/admin"), ApiController]
    public partial class AdminController(IMediator mediator, IMapper mapper) : ControllerBase
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
            try { await this.mediator.Send(this.mapper.Map<EditProfileCommand>(request)); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
        /// <summary>
        /// Удаление профиля пользователя
        /// </summary>
        /// <param name="request">Идентификатор пользователя</param>
        /// <returns>Статус удаления профиля</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProfileHandler([FromQuery] DeleteProfileRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteProfileCommand>(request)); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Данные профиля успешно изменены");
        }
    }
}
