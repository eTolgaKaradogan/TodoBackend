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
    public class TaskReadRepository : ReadRepository<TaskEntity>, ITaskReadRepository
    {
        public TaskReadRepository(TodoBackendDbContext context) : base (context)
        {

        }
    }
}
