using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Auth.Services.Interfaces;
using WebApi.Dtos;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(ITokenService tokenService, IUserService userService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto loginRequest)
        {
            if (!IsRequestValid(loginRequest))
            {
                return Unauthorized();
            }

            var isValidUser = await userService.IsUserRegisteredAsync(loginRequest);

            var loginSuccess = await userService.VerifyLoginAsync(loginRequest);

            if (loginSuccess)
            {
                var token = tokenService.GenerateToken(loginRequest.Username, "Admin");
                return Ok(new { token });
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] NewUserDto request)
        {
            var user = await userService.RegisterUserAsync(request);

            return Ok(user);
        }

        private bool IsRequestValid(LoginRequestDto loginRequest)
        {
            if (loginRequest is null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(loginRequest.Username))
            {
                return false;
            }

            if (string.IsNullOrEmpty(loginRequest.Password))
            {
                return false;
            }

            return true;
        }
    }
}
