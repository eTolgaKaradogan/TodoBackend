using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoBackend.Application.DTOs.Task
{
    public class ListTaskDto
    {
        public int TotalTaskCount { get; set; }
        public List<TaskDto> Tasks { get; set; }
    }
}
