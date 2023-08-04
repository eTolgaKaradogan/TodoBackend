using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs.Task;
using TaskEntity = TodoBackend.Domain.Entities.Task;

namespace TodoBackend.Application.Abstractions.Services
{
    public interface ITaskService
    {
        Task CreateAsync(CreateTaskDto createTask);
        Task<ListTaskDto> GetAllByUserAsync(string userId);
        Task<TaskEntity> GetByIdAsync(string id);
        Task UpdateAsync(string id, string title, string description, DateTime? scheduledDate);
        Task DeleteAsync(string id);
        Task CompleteAsync(string id);
    }
}
