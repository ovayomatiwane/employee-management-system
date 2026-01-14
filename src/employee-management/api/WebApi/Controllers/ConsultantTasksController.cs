using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.QueryData;
using WebApi.Services;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/consultanttasks
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantTasksController(IRoleRatesService rolesRatesService) : ControllerBase
    {
        [HttpPost("Assign")]
        public async Task<IActionResult> CreateAsync([FromBody] AssignConsultantDto assignConsultant)
        {
            var result = await rolesRatesService.AssignTaskToConsultantAsync(assignConsultant);
            return Ok(result);
        }
    }
}
