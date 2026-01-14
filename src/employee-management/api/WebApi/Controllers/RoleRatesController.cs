using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleRatesController(IRoleRatesService roleRatesService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var roleRates = await roleRatesService.GetAllAsync();

            return Ok(roleRates.ToList());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] RoleRateDto roleRate)
        {
            await roleRatesService.UpdateRoleRates(roleRate);
            return Ok();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] RoleRateDto roleRate)
        {
            await roleRatesService.CreateRoleRates(roleRate);
            return Ok();
        }
    }
}
