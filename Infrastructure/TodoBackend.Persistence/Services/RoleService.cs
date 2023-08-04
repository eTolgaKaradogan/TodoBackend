using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoBackend.Application.Abstractions.Services;
using TodoBackend.Application.DTOs.Role;
using TodoBackend.Domain.Entities.Identity;

namespace TodoBackend.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<bool> CreateAsync(string name)
        {
            IdentityResult result = await roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            IdentityResult result = await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public async Task<(List<RoleDto>, int)> GetAll(int page, int size)
        {
            var query = roleManager.Roles;

            IQueryable<AppRole> rolesQuery = null;
            if (page != -1 && size != -1)
                rolesQuery = query.Skip(page * size).Take(size);
            else
                rolesQuery = query;

            return (await query.Select(r => new RoleDto { Id = r.Id, Name = r.Name }).ToListAsync(), query.Count());
        }

        public async Task<(string id, string name)> GetByIdAsync(string id)
        {
            string role = await roleManager.GetRoleIdAsync(new() { Id = id });
            return (id, role);
        }

        public async Task<bool> UpdateAsync(string id, string name)
        {
            var role = await roleManager.FindByIdAsync(id);
            role.Name = name;
            IdentityResult result = await roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
