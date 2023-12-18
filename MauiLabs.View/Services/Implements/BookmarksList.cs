using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Responses;
using MauiLabs.View.Services.Commons;
using MauiLabs.View.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Implements
{
    public partial class BookmarksList : IBookmarksList
    {
        protected internal readonly IApiServiceCommunication apiService = default!;
        public BookmarksList(IApiServiceCommunication apiService) : base() => this.apiService = apiService;

        public virtual async Task<string> AddBookmark(RequestInfo<AddBookmarkRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/bookmarks/addbytoken");
            return await this.apiService.AddDataToServer<AddBookmarkRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<string> DeleteBookmark(RequestInfo<DeleteBookmarkRequestModel> requestModel)
        {
            var requestPath = string.Format("cookingrecipes/bookmarks/deletebytoken");
            return await this.apiService.DeleteDataFromServer<DeleteBookmarkRequestModel>(requestPath, requestModel);
        }
        public virtual async Task<GetBookmarksResponseModel> GetBookmarksById(RequestInfo<GetBookmarksByIdRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/bookmarks/getlist");
            return await this.apiService.GetDataFromServer<GetBookmarksByIdRequestModel, GetBookmarksResponseModel>(requestPath, model);
        }
        public virtual async Task<GetBookmarksResponseModel> GetBookmarks(RequestInfo<GetBookmarksRequestModel> model)
        {
            var requestPath = string.Format("cookingrecipes/bookmarks/getlistbytoken");
            return await this.apiService.GetDataFromServer<GetBookmarksRequestModel, GetBookmarksResponseModel>(requestPath, model);
        }
    }
}
