using FluentValidation;

namespace MauiLabs.Api.Services.Requests.CommentRequests.GetRecipeCommentsList
{
    public sealed class GetRecipeCommentsListValidation : AbstractValidator<GetRecipeCommentsListRequest>
    {
        public GetRecipeCommentsListValidation() : base()
        {
            this.RuleFor(item => item.Skip).Must(item => item >= 0).WithMessage("Значение [Skip] не может быть отрицательным");
            this.RuleFor(item => item.Take).Must(item => item >= 0).WithMessage("Значение [Take] не может быть отрицательным");
            this.RuleFor(item => item.SortingType).IsInEnum().WithMessage("Неверное значение сортировки");
        }
    }
}
