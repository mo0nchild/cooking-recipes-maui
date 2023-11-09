using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks;
using MauiLabs.Api.Controllers.ApiModels.Ingredients.Requests;
using MauiLabs.Api.Controllers.ApiModels.Ingredients.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem;
using MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientItem;
using MauiLabs.Api.Services.Requests.IngredientItemRequests.GetIngredientItems;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/ingredients"), ApiController]
    public partial class IngredientsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление ингредиента для создания рецепта
        /// </summary>
        /// <param name="request">Данные ингредиента</param>
        /// <returns>Статус добавления ингредиента</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddIngredientHandler([FromBody] AddIngredientRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddIngredientItemCommand>(request)); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Ингредиент успешно добавлен");
        }
        /// <summary>
        /// [Admin] Удаление ингредиента для создания рецепта
        /// </summary>
        /// <param name="request">Название ингредиента</param>
        /// <returns>Статус удаления ингредиента</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteIngredientHandler([FromQuery] DeleteIngredientRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteIngredientItemCommand>(request)); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Ингредиент успешно удален");
        }
        /// <summary>
        /// Получение списка доступных ингредиентов
        /// </summary>
        /// <returns>Список доступных ингредиентов</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetIngredientsResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetIngredientsHandler()
        {
            var requestModel = new GetIngredientItemsRequest();
            try { return this.Ok(this.mapper.Map<GetIngredientsResponseModel>(await this.mediator.Send(requestModel))); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
