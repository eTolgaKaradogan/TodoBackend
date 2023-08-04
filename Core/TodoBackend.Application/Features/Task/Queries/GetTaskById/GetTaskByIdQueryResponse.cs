using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.Task.Queries.GetTaskById
{
    public class GetTaskByIdQueryResponse
    {
        public TaskVM Task { get; set; }
    }
}