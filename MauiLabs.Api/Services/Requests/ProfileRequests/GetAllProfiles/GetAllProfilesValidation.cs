using FluentValidation;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles
{
    public sealed class GetAllProfilesValidation : AbstractValidator<GetAllProfilesRequest>
    {
        public GetAllProfilesValidation() : base()
        {
            this.RuleFor(item => item.Skip).Must(item => item >= 0).WithMessage("Значение [Skip] не может быть отрицательным");
            this.RuleFor(item => item.Take).Must(item => item >= 0).WithMessage("Значение [Take] не может быть отрицательным");
        }
    }
}
