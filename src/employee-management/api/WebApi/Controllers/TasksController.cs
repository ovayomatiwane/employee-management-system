using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    // localhost:xxxx/api/tasks
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
    }
}
