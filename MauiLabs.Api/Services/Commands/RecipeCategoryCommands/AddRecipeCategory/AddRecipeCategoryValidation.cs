using FluentValidation;

namespace MauiLabs.Api.Services.Commands.RecipeCategoryCommands.AddRecipeCategory
{
    public sealed class AddRecipeCategoryValidation : AbstractValidator<AddRecipeCategoryCommand>
    {
        public AddRecipeCategoryValidation() : base()
        {
            RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия категории");
        }
    }
}
