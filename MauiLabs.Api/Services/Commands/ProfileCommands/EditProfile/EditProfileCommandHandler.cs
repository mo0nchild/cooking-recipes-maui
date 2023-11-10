using AutoMapper;
using MauiLabs.Api.Services.Commons.Exceptions;
using MauiLabs.Dal;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile
{
    public partial class EditProfileCommandHandler(IDbContextFactory<CookingRecipeDbContext> factory,
        IMapper mapper) : IRequestHandler<EditProfileCommand>
    {
        protected readonly IDbContextFactory<CookingRecipeDbContext> _factory = factory;
        protected readonly IMapper _mapper = mapper;

        public async Task Handle(EditProfileCommand request, CancellationToken cancellationToken)
        {
            var mappedModel = this._mapper.Map<UserProfile>(request);
            using (var dbcontext = await this._factory.CreateDbContextAsync(cancellationToken))
            {
                var profile = await dbcontext.UserProfiles.FirstOrDefaultAsync(item => item.Id == request.Id);
                if (profile == null)
                {
                    throw new ApiServiceException("Пользователь не найден", typeof(EditProfileCommand));
                }
                profile.Surname = mappedModel.Surname;
                profile.Name = mappedModel.Name;
                profile.Image = mappedModel.Image;

                profile.Email = mappedModel.Email;
                await dbcontext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
