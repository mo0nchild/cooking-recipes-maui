using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.IngredientUnits.Responses;
using MauiLabs.Api.Services.Commands.BookmarkCommands.AddBookmark;
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
