using FluentValidation;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.AuthorizationProfile
{
    public sealed class AuthorizationValidation : AbstractValidator<AuthorizationRequest>
    {
        public AuthorizationValidation() : base()
        {
            RuleFor(item => item.Login).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение логина");

            RuleFor(item => item.Password).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение пароля");
        }
    }
}
