using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Profile.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IUserProfile
    {
        public Task<string> EditProfileInfo(string token, EditProfileRequestModel model);
        public Task<string> DeleteProfileInfo(string token);
        public Task<string> ChangeProfilePassword(string token, ChangePasswordRequestModel model);

        public Task<GetProfileInfoResponseModel> GetProfileInfoByToken(string token);
        public Task<GetProfileInfoResponseModel> GetProfileInfo(string token, int id);
        public Task<GetProfilesListResponseModel> GetProfilesList(string token, GetProfilesListRequestModel model);
    }
}
