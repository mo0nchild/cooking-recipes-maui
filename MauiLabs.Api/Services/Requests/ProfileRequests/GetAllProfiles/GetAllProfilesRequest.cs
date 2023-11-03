using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles
{
    public partial class GetAllProfilesRequest : IRequest<ProfileCollection> 
    {
        public int Skip { get; set; } = default!;
        public int Take { get; set; } = default!;
    }
}
