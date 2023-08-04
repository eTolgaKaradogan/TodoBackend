using MediatR;

namespace TodoBackend.Application.Features.AppUser.Commands.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}