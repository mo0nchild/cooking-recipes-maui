using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.Authorization;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.Bookmarks.Responses;
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
    [Route("cookingrecipes/bookmarks"), ApiController, TypeFilter(typeof(ProfileCheckFilter))]
    public partial class BookmarksController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление заметки о рецепте для пользователя
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус добавления заметки о рецепте</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBookmarkHandler([FromBody] AddBookmarkByIdRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddBookmarkCommand>(request)); }
            catch (ValidationException errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Заметка успешно добавлена");
        }
        /// <summary>
        /// Добавление заметки о рецепте для пользователя при помощи токена
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус добавления заметки о рецепте</returns>
        [Route("addbytoken"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddBookmarkByTokenHandler([FromBody] AddBookmarkRequestModel request)
        {
            var mappedRequest = this.mapper.Map<AddBookmarkCommand>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Заметка успешно добавлена");
        }
        /// <summary>
        /// [Admin] Удаление заметки о рецепте для пользователя
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус удаления заметки о рецепте</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookmarkHandler([FromQuery] DeleteBookmarkByIdRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteBookmarkCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Заметка успешно удалена");
        }
        /// <summary>
        /// Удаление заметки о рецепте для пользователя при помощи токена
        /// </summary>
        /// <param name="request">Данные заметки о рецепте</param>
        /// <returns>Статус удаления заметки о рецепте</returns>
        [Route("deletebytoken"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBookmarkByTokenHandler([FromQuery] DeleteBookmarkRequestModel request)
        {
            var mappedRequest = this.mapper.Map<DeleteBookmarkCommand>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBookmarksListByTokenHandler([FromQuery] GetBookmarksRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetBookmarksListRequest>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { return this.Ok(this.mapper.Map<GetBookmarksResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo) 
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
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetBookmarksListHandler([FromQuery] GetBookmarksByIdRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetBookmarksListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetBookmarksResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo) 
            { 
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest); 
            }
        }
    }
}