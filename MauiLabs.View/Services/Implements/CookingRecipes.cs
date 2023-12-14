using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.IngredientUnits.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.RecipeCategory.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace MauiLabs.View.Services.Implements
{
    using WebApiOptions = ConfigureWebApi.WebApiOptions;
    public partial class CookingRecipes : ICookingRecipes
    {
        protected internal readonly IApiServiceCommunication apiService = default!;
        public CookingRecipes(IApiServiceCommunication apiService) : base() => this.apiService = apiService;

        public virtual async Task<GetRecipesListResponseModel> GetRecipesList(RequestInfo<GetRecipesListRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/recipes/getlist");
            return await this.apiService.GetDataFromServer<GetRecipesListRequestModel, GetRecipesListResponseModel>(requestPath, model);
        }
        public virtual async Task<GetRecipeResponseModel> GetRecipeInfo(RequestInfo<GetRecipeRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/recipes/get");
            return await this.apiService.GetDataFromServer<GetRecipeRequestModel, GetRecipeResponseModel>(requestPath, model);
        }
        public virtual async Task<string> AddRecipeInfo(RequestInfo<AddRecipeRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/recipes/addbytoken");
            return await this.apiService.AddDataToServer<AddRecipeRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> DeleteRecipeInfo(RequestInfo<DeleteRecipeRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/recipes/delete");
            return await this.apiService.DeleteDataFromServer<DeleteRecipeRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> EditRecipeInfo(RequestInfo<EditRecipeRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/recipes/edit");
            return await this.apiService.UpdateDataToServer<EditRecipeRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<GetRecipesListResponseModel> GetPublishedListById(RequestInfo<GetPublisherRecipesListByIdRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/recipes/getpublisherlist");
            return await this.apiService
                .GetDataFromServer<GetPublisherRecipesListByIdRequestModel, GetRecipesListResponseModel>(requestPath, model);
        }
        public virtual async Task<GetRecipesListResponseModel> GetPublishedList(RequestInfo<GetPublisherRecipesListRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/recipes/getpublisherlist/bytoken");
            return await this.apiService.GetDataFromServer<GetPublisherRecipesListRequestModel, GetRecipesListResponseModel>(requestPath, model);
        }
        public virtual async Task<GetRecipeCategoriesListResponseModel> GetCategoriesList(string token, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/category/getlist");
            using var request = new HttpRequestMessage(HttpMethod.Get, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync(request, cancelToken, async content =>
            {
                return JsonConvert.DeserializeObject<GetRecipeCategoriesListResponseModel>(await content.ReadAsStringAsync());
            });
        }
        public virtual async Task<GetIngredientUnitsResponseModel> GetUnitsList(string token, CancellationToken cancelToken)
        {
            var requestPath = string.Format("cookingrecipes/units/getlist");
            using var request = new HttpRequestMessage(HttpMethod.Get, requestPath)
            {
                Headers = { { "Authorization", string.Format("Bearer {0}", token) } },
            };
            return await this.apiService.SendRequestAsync(request, cancelToken, async content =>
            {
                return JsonConvert.DeserializeObject<GetIngredientUnitsResponseModel>(await content.ReadAsStringAsync());
            });
        }
    }
}
