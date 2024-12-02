using API.Models;
using API.Models.Dtos;
using CSharpFunctionalExtensions;

namespace API.Services.Interfaces;

public interface IUserService
{
    public Task<UserDto> RegisterUserAsync(RegisterDto registerDto);
    public Task<string> AuthenticateUserAsync(LoginDto loginDto);
    Task<bool> UserExists(string username, string email);
    Task<User> GetUserByUsernameAsync(string username);
}
