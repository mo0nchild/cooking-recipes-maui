using AutoMapper;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile
{
    public class DeleteProfileCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory) 
        : IRequestHandler<DeleteProfileCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(DeleteProfileCommand);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var result = await dbcontext.UserProfiles.Where(item => item.Id == request.Id).ExecuteDeleteAsync();
                if (result <= 0) throw new ApiServiceException("Невозможно удалить профиль", requestType);
            }   
        }
    }
}
