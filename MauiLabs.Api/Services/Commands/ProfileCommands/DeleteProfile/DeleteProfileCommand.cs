using MediatR;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.DeleteProfile
{
    public partial class DeleteProfileCommand : IRequest
    {
        public required int Id { get; set; } = default!;
    }
}
