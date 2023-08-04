using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.Exceptions;
using TodoBackend.Application.Repositories.Task;

namespace TodoBackend.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommandRequest, CreateTaskCommandResponse>
    {
        readonly ITaskService taskService;

        public CreateTaskCommandHandler(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommandRequest request, CancellationToken cancellationToken)
        {
            
            await taskService.CreateAsync(new()
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                ScheduledDate = request.ScheduledDate
            });
            return new();
        }
    }
}
