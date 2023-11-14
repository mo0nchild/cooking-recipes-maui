using AutoMapper;
using FluentValidation;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.ConfirmeCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.DeleteCookingRecipe;
using MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe;
using MauiLabs.Api.Services.Commands.RecommendCommands.DeleteRecommend;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.GetPublishedRecipeList;
using MauiLabs.Api.Services.Requests.CookingRecipeRequests.Models;
using MauiLabs.Api.Services.Requests.RecommendRequests.GetRecommendsList;
using MauiLabs.Api.Services.Requests.RecommendRequests.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MauiLabs.Api.RemoteServices.Implementations.CookingRecipe
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "User")]
    public partial class CookingRecipeServiceCommands(IMediator mediator, IMapper mapper, IHttpContextAccessor accessor)
        : CookingRecipeCommands.CookingRecipeCommandsBase
    {
        protected internal readonly IMediator _mediator = mediator;
        protected internal readonly IMapper _mapper = mapper;

        protected internal IHttpContextAccessor _contextAccessor = accessor;
        public int ProfileId { get => int.Parse(this._contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.PrimarySid)!); }
        public bool ProfileIsAdmin { get => this._contextAccessor.HttpContext!.User.IsInRole("Admin")!; }

        public override async Task<Empty> AddCookingRecipeByToken(AddCookingRecipeByTokenModel request, ServerCallContext context)
        {
            var mappedRequest = this._mapper.Map<AddCookingRecipeCommand>(request);
            mappedRequest.PublisherId = this.ProfileId;

            try { await this._mediator.Send(mappedRequest); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public override async Task<Empty> AddCookingRecipe(AddCookingRecipeModel request, ServerCallContext context)
        {
            try { await this._mediator.Send(this._mapper.Map<AddCookingRecipeCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Admin")]
        public override async Task<Empty> ConfirmeCookingRecipe(ConfirmeCookingRecipeModel request, ServerCallContext context)
        {
            try { await this._mediator.Send(this._mapper.Map<ConfirmeCookingRecipeCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }
        public override async Task<Empty> DeleteCookingRecipe(DeleteCookingRecipeModel request, ServerCallContext context)
        {
            var userFilter = (CookingRecipeInfo info) => info.Id == request.Id;
            try {
                await this.CheckProfileRoleAsync(info => userFilter.Invoke(info));
                await this._mediator.Send(this._mapper.Map<DeleteCookingRecipeCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }
        public override async Task<Empty> EditCookingRecipe(EditCookingRecipeModel request, ServerCallContext context)
        {
            var userFilter = (CookingRecipeInfo info) => info.Id == request.Id;
            try {
                await this.CheckProfileRoleAsync(info => userFilter.Invoke(info));
                await this._mediator.Send(this._mapper.Map<EditCookingRecipeCommand>(request));
            }
            catch (ValidationException errorInfo)
            {
                throw new RpcException(Status.DefaultCancelled, errorInfo.Message);
            }
            return new Empty();
        }
        protected virtual async Task CheckProfileRoleAsync(Func<CookingRecipeInfo, bool> checker)
        {
            var recommends = await this._mediator.Send(new GetPublishedRecipeListRequest() { PublisherId = this.ProfileId });

            if (!this.ProfileIsAdmin && recommends.Recipes.FirstOrDefault(p => checker.Invoke(p)) == null)
            {
                throw new ValidationException("Рецепт не принадлежит пользователю");
            }
        }
    }
}
