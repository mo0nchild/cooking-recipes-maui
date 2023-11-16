using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Controllers.ApiModels.Comments.Requests;
using MauiLabs.Api.Controllers.ApiModels.FriendsList.Requests;
using MauiLabs.Api.Controllers.ApiModels.FriendsList.Responses;
using MauiLabs.Api.Services.Commands.CommentCommands.AddComment;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.FriendCommands.AddFriend;
using MauiLabs.Api.Services.Commands.FriendCommands.DeleteFriend;
using MauiLabs.Api.Services.Requests.FriendRequests.GetFriendsList;
using MauiLabs.Api.Services.Requests.FriendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/friends"), ApiController]
    public partial class FriendsListController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление записи о друзьях
        /// </summary>
        /// <param name="request">Данные двух пользователей</param>
        /// <returns>Статус добавления записи о друзьях</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddFriendHandler([FromBody] AddFriendRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddFriendCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись о друзьях успешно добавлена");
        }
        /// <summary>
        /// Добавление записи о друзьях при помощи токена
        /// </summary>
        /// <param name="request">Данные двух пользователей</param>
        /// <returns>Статус добавления записи о друзьях</returns>
        [Route("addbytoken"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddFriendByTokenHandler([FromBody] AddFriendByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<AddFriendCommand>(request);
            mappedRequest.RequesterId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись о друзьях успешно добавлена");
        }
        /// <summary>
        /// Удаление записи о друзьях с проверкой доступа
        /// </summary>
        /// <param name="request">Данные двух пользователей</param>
        /// <returns>Статус удаления записи о друзьях</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFriendHandler([FromQuery] DeleteFriendRequestModel request)
        {
            var userFilter = (FriendInfo info) => info.Id == request.RecordId;
            var isAdmin = this.HttpContext.User.IsInRole("Admin")!;
            try {
                var friends = await this.mediator.Send(new GetFriendsListRequest() { ProfileId = this.ProfileId });
                if (!isAdmin && friends.Friends.FirstOrDefault(p => userFilter.Invoke(p)) == null)
                {
                    throw new ValidationException("Запись о друге не принадлежит пользователю");
                }
                await this.mediator.Send(this.mapper.Map<DeleteFriendCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись о друзьях успешно удалена");
        }
        /// <summary>
        /// Получение списка друзей пользователя
        /// </summary>
        /// <param name="request">Данные пользователя</param>
        /// <returns>Список друзей пользователя</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetFriendsListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFriendsListHandler([FromQuery] GetFriendsListRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetFriendsListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetFriendsListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка друзей пользователя при помощи токена
        /// </summary>
        /// <returns>Список друзей пользователя</returns>
        [Route("getlistbytoken"), HttpGet]
        [ProducesResponseType(typeof(GetFriendsListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFriendsListByTokenHandler()
        {
            var request = new GetFriendsListRequest() { ProfileId = this.ProfileId };

            try { return this.Ok(this.mapper.Map<GetFriendsListResponseModel>(await this.mediator.Send(request))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
