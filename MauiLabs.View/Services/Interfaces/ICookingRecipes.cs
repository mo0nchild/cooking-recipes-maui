using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
#nullable enable
    public interface ICookingRecipes
    {
        public Task<GetRecipesListResponseModel> GetRecipesList(RequestInfo<GetRecipesListRequestModel> requestModel);
        public Task<GetRecipeResponseModel> GetRecipeInfo(RequestInfo<GetRecipeRequestModel> requestModel);
    }
#nullable disable
}
