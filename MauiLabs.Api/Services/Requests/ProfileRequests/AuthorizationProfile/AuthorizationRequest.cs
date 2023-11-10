using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Controllers.ApiModels.Authorization.Requests;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile
{
    public sealed class AuthorizationRequest : IRequest<AuthorizationInfo?>
    {
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
