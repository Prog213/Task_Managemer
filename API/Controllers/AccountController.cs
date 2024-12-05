using Application.Dtos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] RegisterDto registerDto)
        {
            var user = await userService.RegisterUserAsync(registerDto);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> AuthenticateUser([FromBody] LoginDto loginDto)
        {
            var token = await userService.AuthenticateUserAsync(loginDto);
            return Ok(token);
        }
    }
}
