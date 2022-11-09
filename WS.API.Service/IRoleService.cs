using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WS.API.DTOs.Role;
using WS.API.Models;

namespace WS.API.Service
{
    public interface IRolesService
    {
        Task<Role> GetRoleAsync(Guid idRole);
        Task<Role> GetRoleExistAsync(RoleRequest request);
        Task<IEnumerable<Role>> GetRolesAsync();

        Task CreateRoleAsync(RoleRequest role);

        Task UpdateRoleAsync(Role role);

        Task DeleteRoleAsync(Guid idRole);
    }
}
