using AutoMapper;
using FluentValidation;
using Grpc.Core;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipe;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetCookingRecipesList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.CookingRecipe
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class CookingRecipeServiceRequests(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : CookingRecipeRequests.CookingRecipeRequestsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }

        public override async Task<CookingRecipeInfoModel> GetCookingRecipe(GetCookingRecipeModel request,
            ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<GetCookingRecipeRequest>(request);
            CookingRecipeInfo? requestResult = default!;
            try { 
                requestResult = await this._mediator.Send(mappedRequest);
                if (requestResult == null) throw new ValidationException("Запись рецепта не найдена");
            }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return this._mapper.Map<CookingRecipeInfoModel>(requestResult);
        }

        public override async Task<CookingRecipeListModel> GetCookingRecipesList(GetCookingRecipesListModel request, 
            ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<GetCookingRecipesListRequest>(request);
            try { return this._mapper.Map<CookingRecipeListModel>(await this._mediator.Send(mappedRequest)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
        }

        public override async Task<CookingRecipeListModel> GetPublisherRecipeListByToken(GetPublisherRecipeListByTokenModel request,
            ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<GetPublishedRecipeListRequest>(request);
            mappedRequest.PublisherId = this.ProfileId;
            try { return this._mapper.Map<CookingRecipeListModel>(await this._mediator.Send(mappedRequest)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
        }

        public override async Task<CookingRecipeListModel> GetPublishedRecipeList(GetPublishedRecipeListModel request, 
            ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<GetPublishedRecipeListRequest>(request);
            try { return this._mapper.Map<CookingRecipeListModel>(await this._mediator.Send(mappedRequest)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
        }
    }
}
