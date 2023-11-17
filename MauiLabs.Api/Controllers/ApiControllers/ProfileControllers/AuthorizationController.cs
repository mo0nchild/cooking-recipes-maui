using AutoMapper;
using MauiLabs.Api.Commons.Authentication;
using MauiLabs.Api.Services.Commons.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Text;
using MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile;
using MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile;
using MauiLabs.Api.Controllers.ApiModels.Authorization.Requests;
using MauiLabs.Api.Controllers.ApiModels.Authorization.Responses;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using FluentValidation;

namespace MauiLabs.Api.Controllers.ApiControllers.ProfileControllers
{
    using JwtBearerConfig = ConfigureJwtBearer.JwtBearerConfig;

    [Route("cookingrecipes/auth"), AllowAnonymous]
    [ApiController]
    public partial class AuthorizationController : ControllerBase
    {
        protected readonly IMediator mediator = default!;
        protected readonly IMapper mapper = default!;

        private readonly JwtBearerConfig _tokenConfig = default!;
        public AuthorizationController(IMediator mediator, IMapper mapper, IOptions<JwtBearerConfig> tokenOptions) : base()
        {
            (this.mediator, this.mapper) = (mediator, mapper);
            _tokenConfig = tokenOptions.Value;
        }
        /// <summary>
        /// Авторизация пользователя для дальнейшей работы в системе
        /// </summary>
        /// <param name="request">Данные для авторизации пользователя</param>
        /// <returns>Токен авторизации</returns>
        [Route("login"), HttpGet]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginHandler([FromQuery] LoginRequestModel request)
        {
            AuthorizationInfo? result = default!;
            try {
                result = await mediator.Send(mapper.Map<AuthorizationRequest>(request));
                if (result == null) throw new ValidationException("Пользователь не найден");
            }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            var resultClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, result.Id.ToString()),
                new Claim(ClaimTypes.Role, "User"),
            };
            if (result.IsAdmin) resultClaims.Add(new Claim(ClaimTypes.Role, "Admin"));

            var encodingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.SecretKey));
            var securityToken = new JwtSecurityToken(
                issuer: _tokenConfig.Issuer,
                audience: _tokenConfig.Audience,
                claims: resultClaims,
                signingCredentials: new SigningCredentials(encodingKey, SecurityAlgorithms.HmacSha256));

            return this.Ok(new LoginResponseModel()
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken),
                IsAdmin = result.IsAdmin, ProfileId = result.Id,
            });
        }
        /// <summary>
        /// Регистрация нового пользователя в системе
        /// </summary>
        /// <param name="request">Данные для регистрации пользователя</param>
        /// <returns>Токен авторизации</returns>
        [Route("registration"), HttpPost]
        [ProducesResponseType(typeof(LoginResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrationHandler([FromBody] RegistrationRequestModel request)
        {
            try { await mediator.Send(mapper.Map<RegistrationCommand>(request)); }
            catch (ValidationException errorInfo)
            {
                return this.Problem(errorInfo.Message, statusCode: StatusCodes.Status400BadRequest);
            }
            var newRequest = new LoginRequestModel() { Login = request.Login, Password = request.Password };
            return await this.LoginHandler(newRequest);
        }
    }
}
