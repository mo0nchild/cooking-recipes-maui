using FluentValidation;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe
{
    public partial class AddCookingRecipeValidation : AbstractValidator<AddCookingRecipeCommand>
    {
        public AddCookingRecipeValidation() : base()
        {
            RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия рецепта");
            RuleFor(item => item.Category).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия категории");

            RuleFor(item => item.Ingredients).ForEach(item =>
            {
                item.ChildRules(p => p.RuleFor(i => i.Key).MaximumLength(20).NotEmpty()
                    .WithMessage("Неверное значение названия ингредиента"));
            });
        }
    }
}
