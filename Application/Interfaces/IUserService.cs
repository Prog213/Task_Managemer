using Application.Dtos;
using Domain.Models;

namespace Application.Interfaces;

public interface IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    public Task<string> AuthenticateUserAsync(LoginDto loginDto);
    Task<bool> UserExists(string username, string email);
    Task<User> GetUserByUsernameAsync(string username);
}
