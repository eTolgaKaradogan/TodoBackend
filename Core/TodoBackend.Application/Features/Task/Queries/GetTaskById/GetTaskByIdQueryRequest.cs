using MediatR;

namespace TodoBackend.Application.Features.Task.Queries.GetTaskById
{
    public class GetTaskByIdQueryRequest : IRequest<GetTaskByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}