using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Authorization;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.BookmarkCommands.DeleteBookmark;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetBookmarksList;
using MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using static MauiLabs.Api.Commons.Authentication.ConfigureJwtBearer;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/bookmarks"), ApiController]
    public partial class BookmarksController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// Добавление заметки о рецепте для пользователя
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус добавления заметки о рецепте</returns>
        [Route("add"), HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBookmarkHandler([FromBody] AddBookmarkRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddBookmarkCommand>(request)); }
            catch (Exception errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Заметка успешно добавлена");
        }
        /// <summary>
        /// Удаление заметки о рецепте для пользователя
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус удаления заметки о рецепте</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBookmarkHandler([FromQuery] DeleteBookmarkRequestModel request)
        {
            var mappedRequest = this.mapper.Map<DeleteBookmarkCommand>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Заметка успешно удалена");
        }
        /// <summary>
        /// Получение списка заметок рецептов, добавленных пользователем по токену
        /// </summary>
        /// <param name="request">Параметры сортировки</param>
        /// <returns>Список заметок рецептов пользователя</returns>
        [Route("getlistbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetBookmarksResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBookmarksListHandler([FromQuery] GetBookmarksByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetBookmarksListRequest>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { return this.Ok(await this.mediator.Send(mappedRequest)); }
            catch (Exception errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка заметок рецептов, добавленных пользователем
        /// </summary>
        /// <param name="request">Параметры сортировки</param>
        /// <returns>Список заметок рецептов пользователя</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetBookmarksResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBookmarksListHandler([FromQuery] GetBookmarksRequestModel request)
        {
            try { return this.Ok(await this.mediator.Send(this.mapper.Map<GetBookmarksListRequest>(request))); }
            catch (Exception errorInfo) 
            { 
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest); 
            }
        }
    }
}