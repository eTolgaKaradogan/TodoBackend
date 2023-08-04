using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Application.Features.Task.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommandRequest, UpdateTaskCommandResponse>
    {
        readonly ITaskService taskService;
        public UpdateTaskCommandHandler(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<UpdateTaskCommandResponse> Handle(UpdateTaskCommandRequest request, CancellationToken cancellationToken)
        {             
            await taskService.UpdateAsync(request.Id, request.Title, request.Description, request.ScheduledDate);
            return new();
        }
    }
}
