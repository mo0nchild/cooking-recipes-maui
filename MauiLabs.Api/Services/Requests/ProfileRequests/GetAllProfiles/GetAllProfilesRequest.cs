﻿using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MediatR;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles
{
    public partial class GetAllProfilesRequest : IRequest<ProfileCollection> 
    {
        public string? TextFilter { get; set; } = default!;
        public required int Skip { get; set; } = default!;
        public required int Take { get; set; } = default!;
    }
}
