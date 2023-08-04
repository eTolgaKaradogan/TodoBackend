using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.DTOs.Role;

namespace TodoBackend.Application.Abstractions.Services
{
    public interface IRoleService
    {
        Task<(List<RoleDto>, int)> GetAll(int page, int size);
        Task<(string id, string name)> GetByIdAsync(string id);
        Task<bool> CreateAsync(string name);
        Task<bool> DeleteAsync(string name);
        Task<bool> UpdateAsync(string id, string name);
    }
}
