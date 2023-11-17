using AutoMapper;
using MauiLabs.Api.Services.Requests.ProfileRequests.Models;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace MauiLabs.Api.Services.Requests.ProfileRequests.GetAllProfiles
{
    public partial class GetAllProfilesRequestHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<GetAllProfilesRequest, ProfileCollection>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task<ProfileCollection> Handle(GetAllProfilesRequest request, CancellationToken cancellationToken)
        {
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var profiles = dbcontext.UserProfiles.Where(item => request.TextFilter == null 
                    ? true : Regex.IsMatch(item.Name + " " + item.Surname, request.TextFilter, RegexOptions.IgnoreCase));

                var result = await profiles.Skip(request.Skip).Take(request.Take).ToListAsync();
                return new ProfileCollection() 
                { 
                    AllCount = await profiles.CountAsync(),
                    Profiles = this._mapper.Map<List<ProfileInfo>>(result) 
                };
            }
        }
    }
}
