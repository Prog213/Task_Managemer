using API.Models.Dtos;

namespace API.Services.Interfaces;

public interface IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    public Task<string> AuthenticateUserAsync(LoginDto loginDto);
}
