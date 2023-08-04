using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoBackend.Application.Features.AppUser.Commands.LoginUser;
using TodoBackend.Application.Features.AppUser.Commands.PasswordReset;
using TodoBackend.Application.Features.AppUser.Commands.RefreshTokenLogin;
using TodoBackend.Application.Features.AppUser.Commands.VerifyResetToken;

namespace TodoBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }


        [HttpPost("password-reset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetCommandRequest passwordResetCommandRequest)
        {
            return Ok(await mediator.Send(passwordResetCommandRequest));
        }

        [HttpPost("verify-reset-token")]
        public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
        {
            return Ok(await mediator.Send(verifyResetTokenCommandRequest));
        }
    }
}
