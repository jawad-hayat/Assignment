using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        
        [HttpPost("create_role")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }

            var result = await _adminService.CreateRole(roleName);

            if (result)
            {
                return Ok("Role created successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to create role.");
            }
        }

        [HttpGet("get_roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _adminService.GetRoles();
            return Ok(roles);
        }

        [HttpGet("get_role_by_id/{id}")]
        public async Task<IActionResult> GetRoleByID(string id)
        {
            var role = await _adminService.GetRoleByID(id);
            if (role != null)
            {
                return Ok(role);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("update_role/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string newName)
        {
            var result = await _adminService.UpdateRole(id, newName);
            if (result)
            {
                return Ok("Role updated successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to update role.");
            }
        }

        [HttpDelete("delete_role/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _adminService.DeleteRole(id);
            if (result)
            {
                return Ok("Role deleted successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to delete role.");
            }
        }
    }
}
