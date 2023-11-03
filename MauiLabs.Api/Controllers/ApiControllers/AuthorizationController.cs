using AutoMapper;
using MauiLabs.Api.Commons.Authentication;
using MauiLabs.Api.Controllers.ApiModels.Authorization;
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

namespace MauiLabs.Api.Controllers.ApiControllers
{
    using JwtBearerConfig = MauiLabs.Api.Commons.Authentication.ConfigureJwtBearer.JwtBearerConfig;

    [Route("cookingrecipes/profile"), AllowAnonymous]
    [ApiController]
    public partial class AuthorizationController : ControllerBase
    {
        protected readonly IMediator mediator = default!;
        protected readonly IMapper mapper = default!;

        private readonly JwtBearerConfig _tokenConfig = default!;
        public AuthorizationController(IMediator mediator, IMapper mapper, IOptions<JwtBearerConfig> tokenOptions) : base()
        {
            (this.mediator, this.mapper) = (mediator, mapper);
            this._tokenConfig = tokenOptions.Value;
        }
        /// <summary>
        /// Авторизация пользователя для дальнейшей работы в системе
        /// </summary>
        /// <param name="request">Данные для авторизации пользователя</param>
        /// <returns>Токен авторизации</returns>
        [Route("login"), HttpGet]
        [ProducesResponseType(typeof(LoginResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginHandler([FromQuery] LoginRequestModel request)
        {
            var result = await this.mediator.Send(this.mapper.Map<AuthorizationRequest>(request));
            if (result == null)
            {
                return this.Problem("Пользователь не найден", statusCode: (int)StatusCodes.Status400BadRequest);
            }
            var encodingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._tokenConfig.SecretKey));
            var securityToken = new JwtSecurityToken(
                issuer: this._tokenConfig.Issuer,
                audience: this._tokenConfig.Audience,
                claims: new List<Claim>() { new Claim(ClaimTypes.Sid, result.Value.ToString()) },
                signingCredentials: new SigningCredentials(encodingKey, SecurityAlgorithms.HmacSha256));

            return this.Ok(new LoginResponseModel() 
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(securityToken)
            });
        }
        /// <summary>
        /// Регистрация нового пользователя в системе
        /// </summary>
        /// <param name="request">Данные для регистрации пользователя</param>
        /// <returns>Токен авторизации</returns>
        [Route("registration"), HttpPost]
        [ProducesResponseType(typeof(LoginResponseModel), (int)StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), (int)StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegistrationHandler([FromBody] RegistrationRequestModel request)
        {
            try { await this.mediator.Send(this.mapper.Map<RegistrationCommand>(request)); }
            catch (ApiServiceException errorInfo) 
            {
                return this.Problem(errorInfo.Message, statusCode: (int)StatusCodes.Status400BadRequest);
            }
            var newRequest = new LoginRequestModel() { Login = request.Login, Password = request.Password };
            return await this.LoginHandler(newRequest);
        }
    }
}
