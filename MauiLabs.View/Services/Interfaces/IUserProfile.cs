using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses;
using MauiLabs.View.Services.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IUserProfile
    {
        public Task<string> EditProfileInfo(RequestInfo<EditProfileRequestModel> requestModel);
        public Task<string> ChangeProfilePassword(RequestInfo<ChangePasswordRequestModel> requestModel);
        public Task<string> DeleteProfileInfo(string token, CancellationToken cancelToken);

        public Task<GetProfilesListResponseModel> GetProfilesList(RequestInfo<GetProfilesListRequestModel> requestModel);
        public Task<GetProfileInfoResponseModel> GetProfileInfoByToken(string token, CancellationToken cancelToken);
        public Task<GetProfileInfoResponseModel> GetProfileInfo(string token, int id, CancellationToken cancelToken);
    }
}
