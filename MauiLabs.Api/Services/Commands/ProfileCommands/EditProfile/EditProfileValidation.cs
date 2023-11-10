using FluentValidation;
using MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile;
using System.Text.RegularExpressions;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile
{
    public partial class EditProfileValidation : AbstractValidator<RegistrationCommand>
    {
        public EditProfileValidation() : base()
        {
            this.RuleFor(item => item.Surname).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение фамилии");
            this.RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение имени");

            this.RuleFor(item => item.Email).NotEmpty()
                 .MaximumLength(100)
                 .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                 .WithMessage("Неверный формат почты");
        }
    }
}
