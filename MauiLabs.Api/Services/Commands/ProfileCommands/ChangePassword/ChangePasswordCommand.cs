using MediatR;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword
{
    public partial class ChangePasswordCommand : IRequest
    {
        public required int Id { get; set; } = default!;
        public required string OldPassword { get; set; } = default!;
        public required string NewPassword { get; set; } = default!;
    }
}
