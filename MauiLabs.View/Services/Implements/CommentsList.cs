using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    public partial class CommentsList : ICommentsList
    {
        protected internal readonly IApiServiceCommunication apiService = default!;
        public CommentsList(IApiServiceCommunication apiService) : base() => this.apiService = apiService;

        public virtual async Task<string> AddComment(RequestInfo<AddCommentRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/comments/addbytoken");
            return await this.apiService.AddDataToServer<AddCommentRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> DeleteComment(RequestInfo<DeleteCommentRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/comments/deletebytoken");
            return await this.apiService.DeleteDataFromServer<DeleteCommentRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> EditComment(RequestInfo<EditCommentRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/comments/editbytoken");
            return await this.apiService.UpdateDataToServer<EditCommentRequestModel>(requestPath, requestModel);
        }

        public virtual async Task<GetCommentsResponseModel> GetCommentsList(RequestInfo<GetRecipeCommentsRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/comments/getlist/byrecipe");
            return await this.apiService.GetDataFromServer<GetRecipeCommentsRequestModel, GetCommentsResponseModel>(requestPath, model);
        }
        public virtual async Task<GetCommentResponseModel> GetCommentInfo(RequestInfo<GetCommentRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/comments/getbytoken");
            return await this.apiService.GetDataFromServer<GetCommentRequestModel, GetCommentResponseModel>(requestPath, model);
        }
    }
}
