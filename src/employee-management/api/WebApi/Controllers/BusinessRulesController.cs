using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.QueryData;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessRulesController(IBusinessRulesService businessRulesService) : ControllerBase
    {
        [HttpPut("TotalOwed")]
        public async Task<IActionResult> GetTotalOwed([FromBody] TimeFrameDto timeFrame)
        {
            var totalOwed = await businessRulesService.GetTotalOwedAsync(timeFrame);

            return Ok(totalOwed);
        }
    }
}
