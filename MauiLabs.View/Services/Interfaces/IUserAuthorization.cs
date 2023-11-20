using MauiLabs.Api.Controllers.ApiModels.Authorization.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.View.Services.Interfaces
{
    public interface IUserAuthorization
    {
        public Task AuthorizeUser(string login, string password);
        public Task RegistrationUser(RegistrationRequestModel model);
    }
}
