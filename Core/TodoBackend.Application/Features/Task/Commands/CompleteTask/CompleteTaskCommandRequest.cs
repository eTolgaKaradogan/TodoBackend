using MediatR;

namespace TodoBackend.Application.Features.Task.Commands.CompleteTask
{
    public class CompleteTaskCommandRequest : IRequest<CompleteTaskCommandResponse>
    {
        public string Id { get; set; }
    }
}