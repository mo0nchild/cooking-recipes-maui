using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.Bookmarks;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.IngredientUnits.Requests;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.IngredientUnits.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
using MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientUnit;
using MauiLabs.Api.Services.Commands.IngredientCommands.DeleteIngredientUnit;
using MauiLabs.Api.Services.Requests.IngredientRequests.GetIngredientUnits;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/units"), ApiController, TypeFilter(typeof(ProfileCheckFilter))]
    public partial class IngredientUnitsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        /// <summary>
        /// [Admin] Добавление единицы измерения для создания рецепта
        /// </summary>
        /// <param name="request">Данные единицы измерения</param>
        /// <returns>Статус добавления единицы измерения</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddIngredientHandler([FromBody] AddIngredientUnitRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddIngredientUnitCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Единица измерения успешно добавлена");
        }
        /// <summary>
        /// [Admin] Удаление единицы измерения для создания рецепта
        /// </summary>
        /// <param name="request">Название единицы измерения</param>
        /// <returns>Статус удаления единицы измерения</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteIngredientHandler([FromQuery] DeleteIngredientUnitRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteIngredientUnitCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Единица измерения успешно удалена");
        }
        /// <summary>
        /// Получение списка доступных единиц измерения
        /// </summary>
        /// <returns>Список доступных единиц измерения</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetIngredientUnitsResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetIngredientsHandler()
        {
            var requestModel = new GetIngredientUnitsRequest();
            try { return this.Ok(this.mapper.Map<GetIngredientUnitsResponseModel>(await this.mediator.Send(requestModel))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
