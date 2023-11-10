using FluentValidation;

namespace MauiLabs.Api.Services.Commands.IngredientCommands.AddIngredientUnit
{
    public sealed class AddIngredientUnitValidation : AbstractValidator<AddIngredientUnitCommand>
    {
        public AddIngredientUnitValidation() : base() 
        {
            this.RuleFor(item => item.Name).MaximumLength(20).NotEmpty()
                .WithMessage("Неверное значение единиц измерения");
        }
    }
}
