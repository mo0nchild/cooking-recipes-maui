using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.CookingRecipeCommands.AddCookingRecipe
{
    public partial class AddCookingRecipeCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<AddCookingRecipeCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(AddCookingRecipeCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = _mapper.Map<CookingRecipe>(request);
            var requestType = typeof(AddCookingRecipeCommand);
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                if (await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.PublisherId) == null)
                {
                    throw new ApiServiceException("Пользователь не найден", requestType);
                }
                if (request.Category != null)
                {
                    var category = await dbcontext.RecipeCategories.FirstOrDefaultAsync(item => item.Name == request.Category);
                    if (category == null) throw new ApiServiceException("Категория не найдена", requestType);

                    mappedModel.RecipeCategoryId = category.Id;
                }
                foreach (var ingredient in request.Ingredients)
                {
                    var ingredientUnit = await dbcontext.IngredientUnits.FirstOrDefaultAsync(p => p.Name == ingredient.Value.Unit);
                    if (ingredientUnit == null)
                    {
                        throw new ApiServiceException($"Единица измерения не найдена: {ingredient.Key}", requestType);
                    }
                    mappedModel.Ingredients.Add(new IngredientsList() 
                    {
                        Value = ingredient.Value.Value, Name = ingredient.Key, IngredientUnitId = ingredientUnit.Id 
                    });
                }
                await dbcontext.CookingRecipes.AddRangeAsync(mappedModel);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
