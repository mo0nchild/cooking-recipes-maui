using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests;
using MauiLabs.Api.Controllers.ApiModels.Comments.Requests;
using MauiLabs.Api.Controllers.ApiModels.Comments.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.CommentCommands.AddComment;
using MauiLabs.Api.Services.Commands.CommentCommands.DeleteComment;
using MauiLabs.Api.Services.Commands.CommentCommands.EditComment;
using MauiLabs.Api.Services.Requests.CommentRequests.GetComment;
using MauiLabs.Api.Services.Requests.CommentRequests.GetProfileCommentsList;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using MauiLabs.Api.Services.Requests.CommentRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/comments"), ApiController]
    public partial class CommentsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление комментария к записи о рецепте
        /// </summary>
        /// <param name="request">Данные комментария</param>
        /// <returns>Статус добавления комментария</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCommentHandler([FromBody] AddCommentRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddCommentCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно добавлен");
        }
        /// <summary>
        /// Добавление комментария к записи о рецепте при помощи токена
        /// </summary>
        /// <param name="request">Данные комментария</param>
        /// <returns>Статус добавления комментария</returns>
        [Route("addbytoken"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddCommentByTokenHandler([FromBody] AddCommentByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<AddCommentCommand>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно добавлен");
        }
        /// <summary>
        /// [Admin] Удаление комментария к записи о рецепте
        /// </summary>
        /// <param name="request">Идентификатор комментария</param>
        /// <returns>Статус удаления комментария</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCommentHandler([FromQuery] DeleteCommentRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteCommentCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно удален");
        }
        /// <summary>
        /// Удаление комментария пользователя к записи о рецепте при помощи токена
        /// </summary>
        /// <param name="request">Идентификатор комментария</param>
        /// <returns>Статус удаления комментария</returns>
        [Route("deletebytoken"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCommentByTokenHandler([FromQuery] DeleteCommentByTokenRequestModel request)
        {
            try { await this.mediator.Send(new DeleteCommentCommand() { ProfileId = this.ProfileId, RecipeId = request.RecipeId }); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно удален");
        }
        /// <summary>
        /// [Admin] Изменение комментария к записи о рецепте
        /// </summary>
        /// <param name="request">Данные для изменения комментария</param>
        /// <returns>Статус изменения комментария</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("edit"), HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCommentHandler([FromBody] EditCommentRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<EditCommentCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно изменен");
        }
        /// <summary>
        /// Изменение комментария пользователя к записи о рецепте при помощи токена
        /// </summary>
        /// <param name="request">Данные для изменения комментария</param>
        /// <returns>Статус изменения комментария</returns>
        [Route("editbytoken"), HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditCommentByTokenHandler([FromBody] EditCommentByTokenRequestModel request)
        {
            var mappedResult = this.mapper.Map<EditCommentCommand>(request);
            mappedResult.ProfileId = this.ProfileId;

            try { await this.mediator.Send(mappedResult); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Комментарий успешно изменен");
        }
        /// <summary>
        /// Получение информации о комментарии
        /// </summary>
        /// <param name="request">Данные для получения информации о комментарии</param>
        /// <returns>Информация о комментарии</returns>
        [Route("get"), HttpGet]
        [ProducesResponseType(typeof(GetCommentResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCommentHandler([FromQuery] GetCommentRequestModel request)
        {
            CommentInfo? requestResult = default!;
            try {
                requestResult = await this.mediator.Send(this.mapper.Map<GetCommentRequest>(request));
                if (requestResult == null) throw new ValidationException("Комментарий не найден");
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetCommentResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение информации о комментарии при помощи токена
        /// </summary>
        /// <param name="request">Данные для получения информации о комментарии</param>
        /// <returns>Информация о комментарии</returns>
        [Route("getbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetCommentResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetCommentByTokenHandler([FromQuery] GetCommentByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetCommentRequest>(request);
            mappedRequest.ProfileId = this.ProfileId;

            CommentInfo? requestResult = default!;
            try {
                requestResult = await this.mediator.Send(mappedRequest);
                if (requestResult == null) throw new ValidationException("Комментарий не найден");
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetCommentResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение списка комментарий пользователя
        /// </summary>
        /// <param name="request">Данные для получения списка комментарий пользователя</param>
        /// <returns>Список комментариев</returns>
        [Route("getlist/byprofile"), HttpGet]
        [ProducesResponseType(typeof(GetCommentsResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileCommentsHandler([FromQuery] GetProfileCommentsRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetProfileCommentsListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetCommentsResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка комментарий пользователя при помощи токена
        /// </summary>
        /// <param name="request">Данные для получения списка комментарий пользователя</param>
        /// <returns>Список комментариев</returns>
        [Route("getlistbytoken/byprofile"), HttpGet]
        [ProducesResponseType(typeof(GetCommentsResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetProfileCommentsByTokenHandler([FromQuery] GetProfileCommentsByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetProfileCommentsListRequest>(request);
            mappedRequest.ProfileId = this.ProfileId;

            try { return this.Ok(this.mapper.Map<GetCommentsResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка комментарий рецета
        /// </summary>
        /// <param name="request">Данные для получения списка комментарий рецепта</param>
        /// <returns>Список комментариев</returns>
        [Route("getlist/byrecipe"), HttpGet]
        [ProducesResponseType(typeof(GetCommentsResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecipeCommentsHandler([FromQuery] GetRecipeCommentsRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetRecipeCommentsListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetCommentsResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
