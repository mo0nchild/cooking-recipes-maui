using FluentValidation;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientItem
{
    public sealed class AddIngredientItemValidation : AbstractValidator<AddIngredientItemCommand>
    {
        public AddIngredientItemValidation() : base() 
        {
            this.RuleFor(item => item.UnitName).MaximumLength(50).NotEmpty()
                .WithMessage("Неверное значение единиц измерения");

            this.RuleFor(item => item.IngredientName).MaximumLength(20).NotEmpty()
                .WithMessage("Неверное значение названия ингредиента");
        }
    }
}
