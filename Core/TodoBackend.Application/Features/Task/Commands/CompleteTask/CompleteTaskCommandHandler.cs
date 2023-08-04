using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Application.Features.Task.Commands.CompleteTask
{
    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommandRequest, CompleteTaskCommandResponse>
    {
        readonly ITaskService taskService;
        public CompleteTaskCommandHandler(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<CompleteTaskCommandResponse> Handle(CompleteTaskCommandRequest request, CancellationToken cancellationToken)
        {
            await taskService.CompleteAsync(request.Id);
            return new();
        }
    }
}
