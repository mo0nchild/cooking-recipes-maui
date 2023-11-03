using FluentValidation;
using System.Text.RegularExpressions;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile
{
    public sealed class RegistrationValidation : AbstractValidator<RegistrationCommand>
    {
        public RegistrationValidation() : base() 
        {
            this.RuleFor(item => item.Login).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение логина");
            this.RuleFor(item => item.Password).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение пароля");

            this.RuleFor(item => item.Surname).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение фамилии");
            this.RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение имени");

            this.RuleFor(item => item.Email).NotEmpty()
                 .MaximumLength(100)
                 .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                 .WithMessage("Неверный формат почты");

            this.RuleFor(item => item.Phone)
                .Must(item => item == null || Regex.IsMatch(item, @"^\+7[0-9]{10}$"))
                .WithMessage("Неверный формат номера телефона");
        }
    }
}
