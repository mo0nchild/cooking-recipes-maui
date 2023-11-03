using AutoMapper;
using MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword
{
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class ChangePasswordCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory) 
        : IRequestHandler<ChangePasswordCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var requestType = typeof(ChangePasswordCommand);
            var verifyPassword = (string hashPassword) =>
            {
                try { return BCryptType.Verify(request.OldPassword, hashPassword, false, BCrypt.Net.HashType.SHA384); }
                catch (BCrypt.Net.SaltParseException) { return false; }
            };
            using (var dbcontext = await _factory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.Authorizations.FirstOrDefaultAsync(item => item.UserProfileId == request.Id);
                if (profile == null || !verifyPassword(profile.Password))
                {
                    throw new ApiServiceException("Профиль не найден или введён неверный пароль", requestType);
                }
                profile.Password = BCryptType.HashPassword(request.NewPassword);
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
