using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Repositories.Task;
using TodoBackend.Persistence.Context;
using TaskEntity = TodoBackend.Domain.Entities.Task;

namespace TodoBackend.Persistence.Repositories.Task
{
    public class TaskWriteRepository : WriteRepository<TaskEntity>, ITaskWriteRepository
    {
        public TaskWriteRepository(TodoBackendDbContext context) : base(context)
        {

        }
    }
}
