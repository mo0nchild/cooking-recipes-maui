using FluentValidation;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword
{
    public sealed class ChangePasswordValidation : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidation(): base()
        {
            this.RuleFor(item => item.OldPassword).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное старого значение пароля");

            this.RuleFor(item => item.NewPassword).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное нового значение пароля");
        }
    }
}
