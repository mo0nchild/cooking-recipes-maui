using FluentValidation;

namespace MauiLabs.Api.Services.Commands.CommentCommands.AddComment
{
    public sealed class AddCommentValidation : AbstractValidator<AddCommentCommand>
    {
        public AddCommentValidation() : base()
        {
            RuleFor(item => item.Text).MaximumLength(200).NotEmpty()
                .WithMessage("Неверное значение текста комментария");

            this.RuleFor(item => item.Rating).NotEmpty()
                .Must(item => item <= 5 && item >= 0)
                .WithMessage("Неверное название издания");
        }
    }
}
