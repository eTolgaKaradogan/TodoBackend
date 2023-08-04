using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs;
using TodoBackend.Application.DTOs.Task;
using TodoBackend.Application.ViewModels;
using TaskEntity = TodoBackend.Domain.Entities.Task;

namespace TodoBackend.Application.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<TaskDto, TaskVM>();
            CreateMap<TaskVM, TaskDto>();
            CreateMap<TaskEntity, TaskVM>();
            CreateMap<TokenDto, TokenVM>();
        }
    }
}
