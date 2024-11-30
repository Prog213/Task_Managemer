using API.Models.Dtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController(IUserService userService) : ControllerBase
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody]RegisterDto registerDto)
        {
            try
            {
                var user = await userService.RegisterUserAsync(registerDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUserAsync(LoginDto loginDto)
        {
            try
            {
                var token = await userService.AuthenticateUserAsync(loginDto);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
