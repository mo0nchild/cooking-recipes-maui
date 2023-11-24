using FluentValidation;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe
{
    public partial class AddCookingRecipeValidation : AbstractValidator<AddCookingRecipeCommand>
    {
        public AddCookingRecipeValidation() : base()
        {
            this.RuleFor(item => item.Name).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия рецепта");
            this.RuleFor(item => item.Category).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение названия категории");

            this.RuleFor(item => item.Ingredients).ForEach(item =>
            {
                item.ChildRules(p => p.RuleFor(i => i.Key).MaximumLength(50).NotEmpty()
                    .WithMessage("Неверное значение названия ингредиента"));
                item.ChildRules(p => p.RuleFor(i => i.Value.Unit).MaximumLength(20).NotEmpty()
                    .WithMessage("Неверное значение названия единицы измерения"));
            });
        }
    }
}
