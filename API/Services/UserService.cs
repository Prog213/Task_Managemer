using API.Models;
using API.Models.Dtos;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using AutoMapper;
using BC = BCrypt.Net.BCrypt;

namespace API.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, ITokenProvider tokenProvider) : IUserService
{

    public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
    {
        if(await userRepository.UserExistsAsync(registerDto.Username , registerDto.Email))
        {
            throw new InvalidOperationException("Username or email already exists.");
        }

        var hashedPassword = BC.HashPassword(registerDto.Password);
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        userRepository.AddUser(user);
        if (!await userRepository.SaveAllAsync()) throw new Exception("Failed to register user.");
        return mapper.Map<UserDto>(user);
    }

    public async Task<string> AuthenticateUserAsync(LoginDto loginDto)
    {
        var user = await userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null || !BC.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        return tokenProvider.CreateToken(user);
    }
}
