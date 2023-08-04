using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;

namespace TodoBackend.Application.Features.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommandRequest, DeleteTaskCommandResponse>
    {
        readonly ITaskService taskService;

        public DeleteTaskCommandHandler(ITaskService taskService)
        {
            this.taskService = taskService;
        }

        public async Task<DeleteTaskCommandResponse> Handle(DeleteTaskCommandRequest request, CancellationToken cancellationToken)
        {
            await taskService.DeleteAsync(request.Id);
            return new();
        }
    }
}
