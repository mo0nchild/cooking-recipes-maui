using FluentValidation;

namespace MauiLabs.Api.Services.Commands.RecommendCommands.AddRecommend
{
    public partial class AddRecommendValidation : AbstractValidator<AddRecommendCommand>
    {
        public AddRecommendValidation() : base()
        {
            this.RuleFor(item => item.Text).NotEmpty()
                .MaximumLength(200).WithMessage("Неверное значение текста рекомендации");
        }
    }
}
