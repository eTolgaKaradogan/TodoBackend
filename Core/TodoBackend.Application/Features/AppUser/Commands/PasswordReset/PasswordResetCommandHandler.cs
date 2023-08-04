using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Application.Features.AppUser.Commands.PasswordReset
{
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
    {
        readonly IAuthService authService;

        public PasswordResetCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await authService.PasswordResetAsync(request.Email);
            return new();
        }
    }
}
