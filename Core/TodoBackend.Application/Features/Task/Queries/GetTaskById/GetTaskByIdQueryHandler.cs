using AutoMapper;
using MediatR;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.Task.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQueryRequest, GetTaskByIdQueryResponse>
    {
        readonly ITaskService taskService;
        readonly IMapper mapper;

        public GetTaskByIdQueryHandler(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        public async Task<GetTaskByIdQueryResponse> Handle(GetTaskByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var task = await taskService.GetByIdAsync(request.Id);
            return new()
            {
                Task = mapper.Map<TaskVM>(task)
            };
        }
    }
}
