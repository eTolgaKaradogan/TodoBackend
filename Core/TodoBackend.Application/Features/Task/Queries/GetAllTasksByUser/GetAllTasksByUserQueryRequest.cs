using MediatR;

namespace TodoBackend.Application.Features.Task.Queries.GetAllTasksByUser
{
    public class GetAllTasksByUserQueryRequest : IRequest<GetAllTasksByUserQueryResponse>
    {
        public string Email { get; set; }
        public string? UserId { get; set; }
    }
}