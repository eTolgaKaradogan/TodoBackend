using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs.Task;

namespace TodoBackend.Application.Validators.Tasks
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskValidator()
        {
            var aaa = DateTime.UtcNow.Date.AddDays(-1);
            RuleFor(t => t.ScheduledDate)
                .Must(t => t > DateTime.UtcNow.Date.AddDays(-1))
                .WithMessage("Görev tarihi bugünun tarihinden eski olamaz.");
        }
    }
}
