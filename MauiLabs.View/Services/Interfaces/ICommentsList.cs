using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Requests;
using MauiLabs.View.Services.ApiModels.RecipeModels.Comments.Responses;
using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface ICommentsList
    {
        public Task<string> AddComment(RequestInfo<AddCommentRequestModel> requestModel);
        public Task<string> DeleteComment(RequestInfo<DeleteCommentRequestModel> requestModel);
        public Task<string> EditComment(RequestInfo<EditCommentRequestModel> requestModel);

        public Task<GetCommentsResponseModel> GetCommentsList(RequestInfo<GetRecipeCommentsRequestModel> requestModel);
        public Task<GetCommentResponseModel> GetCommentInfo(RequestInfo<GetCommentRequestModel> requestModel);
    }
}
