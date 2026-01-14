using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/roles
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRolesService rolesService) : ControllerBase
    {
        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid roleId)
        {
            var role = await rolesService.GetRoleAsync(roleId);
            return Ok(role);
        }

        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByNameAsync([FromQuery] string roleName)
        {
            var role = await rolesService.GetByNameAsync(roleName);
            return Ok(role);
        }

        [HttpGet("GeAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await rolesService.GetAllRoleAsync();
            return Ok(roles);
        }
    }
}
