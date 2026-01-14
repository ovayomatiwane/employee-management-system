using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/tasks
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITasksService tasksService) : ControllerBase
    {
        [HttpGet("GetAllByConsultantId")]
        public async Task<IActionResult> GetAllByConsultantIdAsync([FromQuery] Guid consultantId)
        {
            var tasks = await tasksService.GetByConsultantIdAsync(consultantId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] TaskDto task)
        {
            await tasksService.CreateTaskAsync(task);
            return Ok();
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid taskId)
        {
            var task = await tasksService.GetTaskAsync(taskId);
            return Ok(task);
        }

        [HttpGet("GetUnassigned")]
        public async Task<IActionResult> GetUnassigned()
        {
            var task = await tasksService.GetUnassignedAsync();
            return Ok(task);
        }

        [HttpPut("Complete")]
        public async Task<IActionResult> CompleteAsync([FromQuery] Guid taskId)
        {
            await tasksService.RemoveTaskAsync(taskId);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaskDto task)
        {
            await tasksService.UpdateTaskAsync(task);
            return Ok();
        }
    }
}
