using MauiLabs.View.Services.ApiModels.Commons;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.CookingRecipe.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.ConfigureOptions;
using MauiLabs.View.Services.Interfaces;
using Microsoft.Extensions.Options;
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
    }
}
