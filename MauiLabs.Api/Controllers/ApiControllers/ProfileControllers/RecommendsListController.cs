using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks.Requests;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.RecommendsList.Requests;
using MauiLabs.Api.Controllers.ApiModels.ProfileModels.RecommendsList.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend;
using MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers.ProfileControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/recommends"), ApiController, TypeFilter(typeof(ProfileCheckFilter))]
    public partial class RecommendsListController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление рекомендации рецепта для пользователя
        /// </summary>
        /// <param name="request">Данные рекомендации рецепта</param>
        /// <returns>Статус добавления рекомендации рецепта</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecommendHandler([FromBody] AddRecommendByIdRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddRecommendCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Рекомендация успешно добавлена");
        }
        /// <summary>
        /// Добавление рекомендации рецепта для пользователя при помощи токена
        /// </summary>
        /// <param name="request">Данные рекомендации рецепта</param>
        /// <returns>Статус добавления рекомендации рецепта</returns>
        [Route("addbytoken"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecommendByTokenHandler([FromBody] AddRecommendRequestModel request)
        {
            var mappedRequest = this.mapper.Map<AddRecommendCommand>(request);
            mappedRequest.FromUserId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Рекомендация успешно добавлена");
        }
        /// <summary>
        /// Удаление рекомендации рецепта для пользователя c проверкой доступа
        /// </summary>
        /// <param name="request">Данные рекомендации рецепта</param>
        /// <returns>Статус удаления рекомендации рецепта</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRecommendHandler([FromQuery] DeleteRecommendRequestModel request)
        {
            var userFilter = (RecommendInfo info) => info.Id == request.RecordId;
            var isAdmin = this.HttpContext.User.IsInRole("Admin")!;
            try {
                var recommends = await this.mediator.Send(new GetRecommendsListRequest() { ProfileId = this.ProfileId });
                if (!isAdmin && recommends.Recommends.FirstOrDefault(p => userFilter.Invoke(p)) == null)
                {
                    throw new ValidationException("Рекомендация не принадлежит пользователю");
                }
                await this.mediator.Send(this.mapper.Map<DeleteRecommendCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Рекомендация успешно удалена");
        }
        /// <summary>
        /// Получение списка рекомендаций рецептов пользователя
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Список рекомендаций рецептов пользователя</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetRecommendsListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecommendsListHandler([FromQuery] GetRecommendsListRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetRecommendsListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetRecommendsListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка рекомендаций рецептов пользователя при помощи токена
        /// </summary>
        /// <returns>Список рекомендаций рецептов пользователя</returns>
        [Route("getlistbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetRecommendsListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecommendsListByTokenHandler()
        {
            var mappedRequest = new GetRecommendsListRequest() { ProfileId = this.ProfileId };

            try { return this.Ok(this.mapper.Map<GetRecommendsListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
