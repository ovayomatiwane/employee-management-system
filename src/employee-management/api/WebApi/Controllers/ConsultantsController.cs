using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/consultants
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantsController(IConsultantsService consultantsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllConsultantsAsync()
        {
            var consultants = await consultantsService.GetAllConsultantsAsync();
            return Ok(consultants.ToList());
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetByIdAsync([FromQuery] Guid consultantId)
        {
            var consultant = await consultantsService.GetConsultantAsync(consultantId);
            return Ok(consultant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConsultantAsync(ConsultantDto consultantDto)
        {
            await consultantsService.CreateConsultantAsync(consultantDto);
            return Ok();
        }
    }
}
