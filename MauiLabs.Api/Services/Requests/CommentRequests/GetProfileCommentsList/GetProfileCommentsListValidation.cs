using FluentValidation;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetProfileCommentsList
{
    public sealed class GetProfileCommentsListValidation : AbstractValidator<GetProfileCommentsListRequest>
    {
        public GetProfileCommentsListValidation() : base()
        {
            this.RuleFor(item => item.Skip).Must(item => item >= 0).WithMessage("Значение [Skip] не может быть отрицательным");
            this.RuleFor(item => item.Take).Must(item => item >= 0).WithMessage("Значение [Take] не может быть отрицательным");
            this.RuleFor(item => item.SortingType).IsInEnum().WithMessage("Неверное значение сортировки");
        }
    }
}
