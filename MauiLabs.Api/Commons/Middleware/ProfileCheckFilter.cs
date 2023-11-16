using FluentValidation;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles;
using MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MauiLabs.Api.Commons.Middleware
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public partial class ProfileCheckFilter(IMediator mediatorService) : Attribute, IAsyncActionFilter
    {
        protected internal readonly IMediator mediator = mediatorService;
        public virtual async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var profileId = context.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid)!;
            try {
                var requestResult = await this.mediator.Send(new GetProfileInfoRequest() { Id = int.Parse(profileId) });
                if (requestResult == null) throw new ValidationException("Пользователь не был найден в системе");

                await next.Invoke();
            }
            catch (ValidationException) { context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized); }
        }
    }
}
