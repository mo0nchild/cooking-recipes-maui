using AutoMapper;
using MauiLabs.Api.Controllers.ApiModels.Profile.Requests;
using MauiLabs.Api.Controllers.ApiModels.RecipeCategory.Requests;
using MauiLabs.Api.Controllers.ApiModels.RecipeCategory.Responses;
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
    [Route("cookingrecipes/category"), ApiController]
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
        /// <role>Admin</role>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("add"), HttpPost]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddRecipeCategoryHandler([FromBody] AddRecipeCategoryRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<AddRecipeCategoryCommand>(request)); }
            catch (Exception errorInfo)
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
        /// <role>Admin</role>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        [Route("delete"), HttpDelete]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(OkObjectResult), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRecipeCategoryHandler([FromBody] DeleteRecipeCategoryRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<DeleteRecipeCategoryCommand>(request)); }
            catch (Exception errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            return this.Ok("Категория рецептов успешно удалена");
        }
        /// <summary>
        /// [User] Получение списка категорий рецептов
        /// </summary>
        /// <returns>Список категорий</returns>
        /// <role>User</role>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
        [Route("getlist"), HttpGet]
        [ProducesResponseType(typeof(GetRecipeCategoriesListResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetRecipeCategoriesListHandler()
        {
            var requestResult = await this.mediator.Send(new GetCategoryListRequest());
            return this.Ok(this.mapper.Map<GetRecipeCategoriesListResponseModel>(requestResult));
        }
    }
}
