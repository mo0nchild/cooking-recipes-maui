using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Bookmarks.Responses;
using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IBookmarksList
    {
        public Task<string> AddBookmark(RequestInfo<AddBookmarkRequestModel> requestModel);
        public Task<string> DeleteBookmark(RequestInfo<DeleteBookmarkRequestModel> requestModel);

        public Task<GetBookmarksResponseModel> GetBookmarksById(RequestInfo<GetBookmarksByIdRequestModel> requestModel);
        public Task<GetBookmarksResponseModel> GetBookmarks(RequestInfo<GetBookmarksRequestModel> requestModel);
    }
}
