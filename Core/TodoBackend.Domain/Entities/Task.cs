using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Domain.Entities.Common;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
