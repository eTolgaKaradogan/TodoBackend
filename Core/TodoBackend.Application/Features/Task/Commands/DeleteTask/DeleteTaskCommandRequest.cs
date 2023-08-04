using MediatR;

namespace TodoBackend.Application.Features.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandRequest : IRequest<DeleteTaskCommandResponse>
    {
        public string Id { get; set; }
    }
}