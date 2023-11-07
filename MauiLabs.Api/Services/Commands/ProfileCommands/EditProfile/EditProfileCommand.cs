using MauiLabs.Api.Commons.Mapping;
using MauiLabs.Dal.Entities;
using MediatR;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.EditProfile
{
    public partial class EditProfileCommand : IRequest, IMappingTarget<UserProfile>
    {
        public required int Id { get; set; } = default!;
        public required string Name { get; set; } = default!;
        public required string Surname { get; set; } = default!;

        public required string Email { get; set; } = default!;
        public string? Phone { get; set; } = default!;
        public byte[]? Image { get; set; } = default!;
    }
}
