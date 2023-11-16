using FluentValidation;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe
{
    public sealed class EditCookingRecipeValidation : AbstractValidator<EditCookingRecipeCommand>
    {
        public EditCookingRecipeValidation() : base()
        {
            RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия рецепта");
            RuleFor(item => item.Category).MaximumLength(50)
                .WithMessage("Неверное значение названия категории");

            RuleFor(item => item.Ingredients).ForEach(item =>
            {
                item.ChildRules(p => p.RuleFor(i => i.Key).MaximumLength(50).NotEmpty()
                    .WithMessage("Неверное значение названия ингредиента"));
                item.ChildRules(p => p.RuleFor(i => i.Value.Unit).MaximumLength(20).NotEmpty()
                    .WithMessage("Неверное значение названия единицы измерения"));
            });
        }
    }
}
