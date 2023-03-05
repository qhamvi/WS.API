using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS.API.DTO.Response;
using WS.API.DTO.Role;
using WS.API.Extensions;
using WS.API.Models;
using WS.API.Service;
using WS.API.Service.Implements;

namespace WS.API.Controllers
{

    [ApiController]
    [Route("roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;
        public RolesController(IRoleService service)
        {
            _service = service;
        }

        //GET /roles
        [HttpGet]
        public async Task<IEnumerable<RoleResponse>> GetRolesAsync()
        {
            var roles = (await _service.GetRolesAsync()).Select(role => role.AsRoleDto());
            return roles;
        }

        //GET /roles/{id}
        [HttpGet("{idRole}")]
        public async Task<ActionResult<RoleResponse>> GetRole(Guid idRole)
        {
            var role = await _service.GetRoleAsync(idRole);
            if (role is null)
            {
                return NotFound();
            }
            return role.AsRoleDto();
        }

        //POST /roles
        [HttpPost]
        public async Task<ActionResult<RoleResponse>> CreateRole(RoleRequest request)
        {
            var existingRole = await _service.GetRoleExistAsync(request);
            if(existingRole != null)
            {
                return Conflict(new ReponseMessage()
                {
                    message = "Role existed"
                });
            }
            await _service.CreateRoleAsync(request);
            return Ok(request);
        }

        //PUT /roles
        [HttpPut("{idRole}")]
        public async Task<ActionResult> UpdateRoleAsync(Guid idRole, RoleRequest request)
        {
            var existingRole = await _service.GetRoleAsync(idRole);
            if (existingRole is null)
            {
                return NotFound();
            }
            Role updateRole = existingRole with
            {
                NameRole = request.NameRole
            };
            await _service.UpdateRoleAsync(updateRole);
            return Content("Update Success");
        }

        //DELETE /roles/{idRole}
        [HttpDelete("{idRole}")]
        public async Task<ActionResult> DeleteRoleAsync(Guid idRole)
        {
            var existingRole = await _service.GetRoleAsync(idRole);
            if (existingRole is null)
            {
                return NotFound();
            }
            await _service.DeleteRoleAsync(idRole);
            return NoContent();
        }
    }
}
