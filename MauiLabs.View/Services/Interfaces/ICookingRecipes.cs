using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.IngredientUnits.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.RecipeCategory.Responses;
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

        public Task<GetRecipesListResponseModel> GetPublishedListById(RequestInfo<GetPublisherRecipesListByIdRequestModel> requestModel);
        public Task<GetRecipesListResponseModel> GetPublishedList(RequestInfo<GetPublisherRecipesListRequestModel> requestModel);

        public Task<string> AddRecipeInfo(RequestInfo<AddRecipeRequestModel> requestModel);
        public Task<string> DeleteRecipeInfo(RequestInfo<DeleteRecipeRequestModel> requestModel);
        public Task<string> EditRecipeInfo(RequestInfo<EditRecipeRequestModel> requestModel);

        public Task<GetRecipeCategoriesListResponseModel> GetCategoriesList(string token, CancellationToken cancelToken);
        public Task<GetIngredientUnitsResponseModel> GetUnitsList(string token, CancellationToken cancelToken);
    }
#nullable disable
}
