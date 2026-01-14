using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    // localhost:xxxx/api/roles
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
    }
}
