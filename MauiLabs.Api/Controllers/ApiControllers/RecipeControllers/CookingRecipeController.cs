using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Controllers.ApiModels.CookingRecipe.Requests;
using MauiLabs.Api.Controllers.ApiModels.CookingRecipe.Responses;
using MauiLabs.Api.Controllers.ApiModels.FriendsList.Requests;
using MauiLabs.Api.RemoteServices.Implementations.CookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe;
using MauiLabs.Api.Services.Commands.FriendCommands.AddFriend;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers.RecipeControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/recipes"), ApiController]
    public partial class CookingRecipeController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        protected virtual async Task CheckProfileRoleAsync(Func<CookingRecipeInfo, bool> checker)
        {
            var recommends = await this.mediator.Send(new GetPublishedRecipeListRequest() { PublisherId = this.ProfileId });

            if (!this.HttpContext.User.IsInRole("Admin")! && recommends.Recipes.FirstOrDefault(p => checker.Invoke(p)) == null)
            {
                throw new ValidationException("Рецепт не принадлежит пользователю");
            }
        }
        /// <summary>
        /// [Admin] Добавление кулинарного рецепта
        /// </summary>
        /// <param name="request">Данные кулинарного рецепта</param>
        /// <returns>Статус добавления кулинарного рецепта</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecipeHandler([FromBody] AddRecipeRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddCookingRecipeCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись кулинарного рецепта успешно добавлена");
        }
        /// <summary>
        /// Добавление кулинарного рецепта при помощи токена
        /// </summary>
        /// <param name="request">Данные кулинарного рецепта</param>
        /// <returns>Статус добавления кулинарного рецепта</returns>
        [Route("addbytoken"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecipeByTokenHandler([FromBody] AddRecipeByTokenRequestModel request)
        {
            var mappedRequest = this.mapper.Map<AddCookingRecipeCommand>(request);
            mappedRequest.PublisherId = this.ProfileId;

            try { await this.mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись кулинарного рецепта успешно добавлена");
        }
        /// <summary>
        /// [Admin] Изменение статуса кулинарного рецепта
        /// </summary>
        /// <param name="request">Данные кулинарного рецепта</param>
        /// <returns>Статус изменения кулинарного рецепта</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("confirme"), HttpPatch]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecipeByTokenHandler([FromQuery] ConfirmeRecipeRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<ConfirmeCookingRecipeCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Статус кулинарного рецепта успешно изменен");
        }
        /// <summary>
        /// Удаление кулинарного рецепта с проверкой доступа
        /// </summary>
        /// <param name="request">Данные кулинарного рецепта</param>
        /// <returns>Статус удаления кулинарного рецепта</returns>
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRecipeHandler([FromQuery] DeleteRecipeRequestModel request)
        {
            var userFilter = (CookingRecipeInfo info) => info.Id == request.Id;
            try {
                await this.CheckProfileRoleAsync(info => userFilter.Invoke(info));
                await this.mediator.Send(this.mapper.Map<DeleteCookingRecipeCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись кулинарного рецепта успешно удалена");
        }
        /// <summary>
        /// Редактирование кулинарного рецепта с проверкой доступа
        /// </summary>
        /// <param name="request">Данные кулинарного рецепта</param>
        /// <returns>Статус редактирования кулинарного рецепта</returns>
        [Route("edit"), HttpPut]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> EditRecipeHandler([FromBody] EditRecipeRequestModel request)
        {
            var userFilter = (CookingRecipeInfo info) => info.Id == request.Id;
            try {
                await this.CheckProfileRoleAsync(info => userFilter.Invoke(info));
                await this.mediator.Send(this.mapper.Map<EditCookingRecipeCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Запись кулинарного рецепта успешно отредактирована");
        }
        /// <summary>
        /// Получение информации о кулинарном рецепте
        /// </summary>
        /// <param name="request">Идентификатор рецепта</param>
        /// <returns>Информация о кулинарном рецепте</returns>
        [Route("get"), HttpGet]
        [ProducesResponseType(typeof(GetRecipeResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecipeHandler([FromQuery] GetRecipeRequestModel request)
        {
            CookingRecipeInfo? requestResult = default!;
            try {
                requestResult = await this.mediator.Send(this.mapper.Map<GetCookingRecipeRequest>(request));
                if (requestResult == null) throw new ValidationException("Запись рецепта не найдена");
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok(this.mapper.Map<GetRecipeResponseModel>(requestResult));
        }
        /// <summary>
        /// Получение списка кулинарных рецептов
        /// </summary>
        /// <param name="request">Настройки поиска рецептов</param>
        /// <returns>Список кулинарных рецептов</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetRecipesListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecipesListHandler([FromQuery] GetRecipesListRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetCookingRecipesListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetRecipesListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка кулинарных рецептов публикатора
        /// </summary>
        /// <param name="request">Идентификатор пользователя</param>
        /// <returns>Список кулинарных рецептов</returns>
        [Route("getpublisherlist"), HttpGet]
        [ProducesResponseType(typeof(GetRecipesListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPublisherRecipesListHandler([FromQuery] GetPublisherRecipesListRequestModel request)
        {
            var mappedRequest = this.mapper.Map<GetPublishedRecipeListRequest>(request);

            try { return this.Ok(this.mapper.Map<GetRecipesListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
        /// <summary>
        /// Получение списка кулинарных рецептов публикатора при помощи токена
        /// </summary>
        /// <returns>Список кулинарных рецептов</returns>
        [Route("getpublisherlist/bytoken"), HttpGet]
        [ProducesResponseType(typeof(GetRecipesListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetPublisherRecipesListByTokenHandler()
        {
            var mappedRequest = new GetPublishedRecipeListRequest() { PublisherId = this.ProfileId };

            try { return this.Ok(this.mapper.Map<GetRecipesListResponseModel>(await this.mediator.Send(mappedRequest))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
