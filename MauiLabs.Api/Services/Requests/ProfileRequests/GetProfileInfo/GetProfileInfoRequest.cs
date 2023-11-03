using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetProfileInfo
{
    public partial class GetProfileInfoRequest : IRequest<ProfileInfo?>, IMappingTarget<ProfileInfo>
    {
        public int Id { get; set; } = default!;
    }
}
