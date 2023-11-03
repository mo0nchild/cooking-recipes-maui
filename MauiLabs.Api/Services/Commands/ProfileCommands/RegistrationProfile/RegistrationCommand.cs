using AutoMapper;
using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.RegistrationProfile
{
    using BCryptType = BCrypt.Net.BCrypt;
    public partial class RegistrationCommand : IRequest, IMappingTarget<UserProfile>
    {
        public string Name { get; set; } = default!;
        public string Surname { get; set; } = default!;

        public string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;

        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;

        public virtual void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<RegistrationCommand, UserProfile>()
                .ForMember(p => p.Authorization, options => options.MapFrom(p => new Authorization()
                {
                    Login = p.Login,
                    Password = BCryptType.HashPassword(p.Password),
                }))
                .ReverseMap();
        }
    }
}
