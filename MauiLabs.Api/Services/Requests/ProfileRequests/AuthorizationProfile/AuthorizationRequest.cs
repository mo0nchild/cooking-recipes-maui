using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Authorization;
using MediatR;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile
{
    public sealed class AuthorizationRequest : IRequest<int?>, IMappingTarget<LoginRequestModel>
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<AuthorizationRequest, LoginRequestModel>();
        }
    }
}
