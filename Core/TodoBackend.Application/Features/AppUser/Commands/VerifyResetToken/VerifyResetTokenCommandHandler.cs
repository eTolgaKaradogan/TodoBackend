using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Application.Features.AppUser.Commands.VerifyResetToken
{
    public class VerifyResetTokenCommandHandler : IRequestHandler<VerifyResetTokenCommandRequest, VerifyResetTokenCommandResponse>
    {
        readonly IAuthService authService;

        public VerifyResetTokenCommandHandler(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<VerifyResetTokenCommandResponse> Handle(VerifyResetTokenCommandRequest request, CancellationToken cancellationToken)
        {
            bool state = await authService.VerifyResetToken(request.ResetToken, request.UserId);
            return new()
            {
                State = state
            };
        }
    }
}
