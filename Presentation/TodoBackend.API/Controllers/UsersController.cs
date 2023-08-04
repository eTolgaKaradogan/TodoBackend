using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoBackend.Application.Features.AppUser.Commands.CreateUser;
using TodoBackend.Application.Features.AppUser.Commands.UpdatePassword;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator mediator;
       
        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserCommandRequest)
        {
            return Ok(await mediator.Send(createUserCommandRequest));
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            return Ok(await mediator.Send(updatePasswordCommandRequest));
        }
    }
}
