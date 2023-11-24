using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.EditCookingRecipe
{
    public partial class EditCookingRecipeCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory)
        : IRequestHandler<EditCookingRecipeCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(EditCookingRecipeCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(EditCookingRecipeCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var cookingrecipe = await dbcontext.CookingRecipes.FirstOrDefaultAsync(item => item.Id == request.Id);
                if (cookingrecipe == null) throw new ApiServiceException("Рецепт не найден", requestType);

                cookingrecipe.Image = request.Image;
                cookingrecipe.Name = request.Name;
                cookingrecipe.Description = request.Description;

                var category = await dbcontext.RecipeCategories.FirstOrDefaultAsync(item => item.Name == request.Category);
                if (category == null) throw new ApiServiceException("Категория не найдена", requestType);

                cookingrecipe.RecipeCategoryId = category.Id;
                foreach (var ingredient in request.Ingredients)
                {
                    await dbcontext.Ingredients.Include(item => item.IngredientUnit).Where(p => p.CookingRecipeId == cookingrecipe.Id
                        && ingredient.Key != p.Name).ExecuteDeleteAsync();

                    var ingredientUnit = await dbcontext.IngredientUnits.FirstOrDefaultAsync(p => p.Name == ingredient.Value.Unit);
                    if (ingredientUnit == null)
                    {
                        throw new ApiServiceException($"Единица измерения не найдена: {ingredient.Value.Unit}", requestType);
                    }
                    cookingrecipe.Ingredients.Add(new IngredientsList()
                    {
                        Value = ingredient.Value.Value, Name = ingredient.Key, IngredientUnitId = ingredientUnit.Id
                    });
                }
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
