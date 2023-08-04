using TodoBackend.Application.DTOs.Task;
using TodoBackend.Application.Validators.Tasks;
using TodoBackend.Application.ViewModels;

namespace TodoBackend.Application.Features.Task.Queries.GetAllTasksByUser
{
    public class GetAllTasksByUserQueryResponse
    {
        public List<TaskVM> Tasks { get; set; }
    }
}