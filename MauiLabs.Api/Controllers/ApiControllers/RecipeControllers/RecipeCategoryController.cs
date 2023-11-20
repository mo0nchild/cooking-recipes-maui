using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.Profile.Requests;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.RecipeCategory.Requests;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.RecipeCategory.Responses;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory;
using MauiLabs.Api.Services.Commands.RecipeCategoryCommands.DeleteRecipeCategory;
using MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList;
using MauiLabs.Api.Services.Requests.RecipeCategoryRequests.GetRecipeCategoryList;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace MauiLabs.Api.Controllers.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    [Route("cookingrecipes/category"), ApiController, TypeFilter(typeof(ProfileCheckFilter))]
    public partial class RecipeCategoryController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        protected internal readonly IMediator mediator = mediator;
        protected internal readonly IMapper mapper = mapper;
        public int ProfileId { get => int.Parse(this.User.FindFirstValue(ClaimTypes.PrimarySid)!); }
        /// <summary>
        /// [Admin] Добавление новой категории для рецептов
        /// </summary>
        /// <param name="request">Название категории</param>
        /// <returns>Статус добавления категории</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecipeCategoryHandler([FromBody] AddRecipeCategoryRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddRecipeCategoryCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Категория рецептов успешно добавлена");
        }
        /// <summary>
        /// [Admin] Удаление категории для рецептов
        /// </summary>
        /// <param name="request">Название категории</param>
        /// <returns>Статус удаления категории</returns>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRecipeCategoryHandler([FromQuery] DeleteRecipeCategoryRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteRecipeCategoryCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Категория рецептов успешно удалена");
        }
        /// <summary>
        /// Получение списка категорий рецептов
        /// </summary>
        /// <returns>Список категорий</returns>
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetRecipeCategoriesListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecipeCategoriesListHandler()
        {
            var requestModel = new GetCategoryListRequest();
            try { return this.Ok(this.mapper.Map<GetRecipeCategoriesListResponseModel>(await this.mediator.Send(requestModel))); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
        }
    }
}
