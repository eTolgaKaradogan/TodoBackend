using MediatR;

namespace TodoBackend.Application.Features.Task.Commands.CreateTask
{
    public class CreateTaskCommandRequest : IRequest<CreateTaskCommandResponse>
    {
        public string? UserId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ScheduledDate { get; set; }

    }
}