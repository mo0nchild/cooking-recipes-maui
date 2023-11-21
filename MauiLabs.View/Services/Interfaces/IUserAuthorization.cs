
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Requests;
using MauiLabs.View.Services.ApiModels.ProfileModels.Authorization.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IUserAuthorization
    {
        public Task<LoginResponseModel> RegistrationUser(RegistrationRequestModel model);
        public Task<LoginResponseModel> AuthorizeUser(LoginRequestModel model);
    }
}
