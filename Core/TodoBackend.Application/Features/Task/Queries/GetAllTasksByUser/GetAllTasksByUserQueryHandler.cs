using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.DTOs.Task;
using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.Task.Queries.GetAllTasksByUser
{
    public class GetAllTasksByUserQueryHandler : IRequestHandler<GetAllTasksByUserQueryRequest, GetAllTasksByUserQueryResponse>
    {
        readonly ITaskService taskService;
        readonly IMapper mapper;

        public GetAllTasksByUserQueryHandler(ITaskService taskService, IMapper mapper)
        {
            this.taskService = taskService;
            this.mapper = mapper;
        }

        public async Task<GetAllTasksByUserQueryResponse> Handle(GetAllTasksByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await taskService.GetAllByUserAsync(request.UserId);
            return new()
            {
                Tasks = mapper.Map<List<TaskDto>, List<TaskVM>>(data.Tasks)
            };
        }
    }
}
