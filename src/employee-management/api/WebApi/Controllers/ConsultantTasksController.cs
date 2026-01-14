using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/consultanttasks
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantTasksController : ControllerBase
    {
    }
}
