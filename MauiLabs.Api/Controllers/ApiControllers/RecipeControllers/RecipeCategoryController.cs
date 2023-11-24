using AutoMapper;
using FluentValidation;
using MauiLabs.Api.Commons.Middleware;
using MauiLabs.Api.Controllers.ApiModels.RecipeModels.RecipeCategory.Responses;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
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
