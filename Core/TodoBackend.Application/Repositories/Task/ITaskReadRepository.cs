using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEntity = TodoBackend.Domain.Entities.Task;

namespace TodoBackend.Application.Repositories.Task
{
    public interface ITaskReadRepository : IReadRepository<TaskEntity>
    {
    }
}
