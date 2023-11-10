using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;
using Microsoft.Extensions.Options;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile
{
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class RegistrationCommand : IRequest, IMappingTarget<UserProfile>
    {
        public required string Name { get; set; } = default!;
        public required string Surname { get; set; } = default!;

        public required string Email { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;
        public required string Login { get; set; } = default!;
        public required string Password { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<RegistrationCommand, UserProfile>()
                .ForMember(p => p.Authorization, options => options.MapFrom(p => new Authorization()
                {
                    Login = p.Login,
                    Password = BCryptType.HashPassword(p.Password),
                }))
                .ForMember(p => p.ReferenceLink, options => options.MapFrom(p => BCryptType.HashPassword(p.Login)));
        }
    }
}
