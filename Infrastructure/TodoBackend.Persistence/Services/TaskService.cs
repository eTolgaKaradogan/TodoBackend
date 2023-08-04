using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.DTOs.Task;
using TodoBackend.Application.Exceptions;
using TodoBackend.Application.Repositories.Task;
using TodoBackend.Application.Validators.Tasks;
using TaskEntity = TodoBackend.Domain.Entities.Task;

namespace TodoBackend.Persistence.Services
{
    public class TaskService : ITaskService
    {
        readonly ITaskWriteRepository taskWriteRepository;
        readonly ITaskReadRepository taskReadRepository;
        readonly CreateTaskValidator createTaskValidator = new();

        public TaskService(ITaskWriteRepository taskWriteRepository, ITaskReadRepository taskReadRepository)
        {
            this.taskWriteRepository = taskWriteRepository;
            this.taskReadRepository = taskReadRepository;
        }

        public async Task CompleteAsync(string id)
        {
            TaskEntity task = await GetByIdAsync(id);
            if (task != null)
            {
                task.CompletedDate = DateTime.UtcNow;
                taskWriteRepository.Update(task);
                await taskWriteRepository.SaveAsync();
            }
        }

        public async Task CreateAsync(CreateTaskDto createTask)
        {
            ValidationResult result = createTaskValidator.Validate(createTask);
            if (!result.IsValid)
                throw new CreateProductValidationException(result.Errors[0].ErrorMessage);
            await taskWriteRepository.AddAsync(new()
            {
                UserId = createTask.UserId,
                Title = createTask.Title,
                Description = createTask.Description,
                ScheduledDate = createTask.ScheduledDate,
                CompletedDate = null
            });
            await taskWriteRepository.SaveAsync();
        }

        public async Task DeleteAsync(string id)
        {
            await taskWriteRepository.Remove(id);
            await taskWriteRepository.SaveAsync();
        }

        public async Task<ListTaskDto> GetAllByUserAsync(string userId)
        {
            var query = taskReadRepository.Table
                .Include(t => t.User);
            var data = query.Where(t => t.UserId == userId).OrderByDescending(t => t.CreatedDate);

            return new()
            {
                Tasks = await data.Select(t => new TaskDto()
                {
                    Id = t.Id.ToString(),
                    UserId = t.UserId,
                    Title = t.Title,
                    Description = t.Description,
                    ScheduledDate = t.ScheduledDate,
                    CreatedDate = t.CreatedDate,
                    UpdatedDate = t.UpdatedDate,
                    CompletedDate = t.CompletedDate
                }).ToListAsync()
            };
        }

        public async Task<TaskEntity> GetByIdAsync(string id)
        {
            var query = taskReadRepository.Table;
            var data = await (from task in query
                              select new
                              {
                                  Id = task.Id,
                                  Title = task.Title,
                                  UserId = task.UserId,
                                  Description = task.Description,
                                  ScheduledDate = task.ScheduledDate,
                                  CreatedDate = task.CreatedDate,
                                  UpdatedDate = task.UpdatedDate,
                                  CompletedDate = task.CompletedDate
                              }).FirstOrDefaultAsync(t => t.Id == Guid.Parse(id));
            return new()
            {
                Id = data.Id,
                UserId = data.UserId,
                Title = data.Title,
                Description = data.Description,
                ScheduledDate = data.ScheduledDate,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
                CompletedDate = data.CompletedDate
            };
        }

        public async Task UpdateAsync(string id, string title, string description, DateTime? scheduledDate)
        {
            var task = await GetByIdAsync(id);
            if (task != null)
            {
                task.Title = title;
                task.Description = description;
                task.ScheduledDate = scheduledDate;
                taskWriteRepository.Update(task);
                await taskWriteRepository.SaveAsync();
            }
        }
    }
}
