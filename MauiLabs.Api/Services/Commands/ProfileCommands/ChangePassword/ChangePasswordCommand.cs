using MediatR;

namespace MauiLabs.Api.Services.Commands.ProfileCommands.ChangePassword
{
    public partial class ChangePasswordCommand : IRequest
    {
        public int Id { get; set; } = default!;
        public string OldPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
    }
}
