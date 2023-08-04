using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoBackend.Application.Exceptions;
using TodoBackend.Application.Features.Task.Commands.CompleteTask;
using TodoBackend.Application.Features.Task.Commands.CreateTask;
using TodoBackend.Application.Features.Task.Commands.DeleteTask;
using TodoBackend.Application.Features.Task.Commands.UpdateTask;
using TodoBackend.Application.Features.Task.Queries.GetAllTasksByUser;
using TodoBackend.Application.Features.Task.Queries.GetTaskById;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        readonly IMediator mediator;
        readonly UserManager<AppUser> userManager;

        public TasksController(IMediator mediator, UserManager<AppUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllTasksByUserQueryRequest getAllTasksQueryRequest)
        {
            AppUser user = await userManager.FindByEmailAsync(getAllTasksQueryRequest.Email);
            if (user == null)
                throw new NotFoundUserException();
            getAllTasksQueryRequest.UserId = user.Id;
            return Ok(await mediator.Send(getAllTasksQueryRequest));
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> Get([FromQuery] GetTaskByIdQueryRequest getTaskByIdQueryRequest)
        {
            return Ok(await mediator.Send(getTaskByIdQueryRequest));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommandRequest createTaskCommandRequest)
        {
            AppUser user = await userManager.FindByEmailAsync(createTaskCommandRequest.Email);
            createTaskCommandRequest.UserId = user.Id;
            await mediator.Send(createTaskCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTaskCommandRequest updateTaskCommandRequest)
        {
            return Ok(await mediator.Send(updateTaskCommandRequest));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteTaskCommandRequest deleteTaskCommandRequest)
        {
            return Ok(await mediator.Send(deleteTaskCommandRequest));
        }

        [HttpPut("complete-task")]
        public async Task<IActionResult> CompleteTask([FromBody] CompleteTaskCommandRequest completeTaskCommandRequest)
        {
            return Ok(await mediator.Send(completeTaskCommandRequest));
        }
    }
}
