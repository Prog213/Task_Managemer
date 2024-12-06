using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, ITokenProvider tokenProvider,
    ILogger<UserService> logger) : IUserService
{
    public async Task<UserDto> RegisterUserAsync(RegisterDto registerDto)
    {
        logger.LogInformation("Registering new user with username {username} and email {email}",
            registerDto.Username, registerDto.Email);

        // Check if user with the same username or email already exists
        if (await UserExists(registerDto.Username, registerDto.Email))
        {
            throw new ArgumentException("Username or email already exists.");
        }

        // Hashing the password and create a new user
        var hashedPassword = BC.HashPassword(registerDto.Password);
        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Add user to the repository and save changes
        userRepository.AddUser(user);
        await userRepository.SaveAllAsync();

        // Return the user DTO
        return mapper.Map<UserDto>(user);
    }

    public async Task<string> AuthenticateUserAsync(LoginDto loginDto)
    {
        // Get user by username and verify the password
        var user = await userRepository.GetUserByUsernameAsync(loginDto.Username);
        if (user == null || !BC.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        // Update the user's last login date and save changes
        user.UpdatedAt = DateTime.UtcNow;
        await userRepository.SaveAllAsync();

        // Return the JWT token
        return tokenProvider.CreateToken(user);
    }

    public async Task<bool> UserExists(string username, string email)
    {
        return await userRepository.UserExistsAsync(username, email);
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await userRepository.GetUserByUsernameAsync(username)
            ?? throw new KeyNotFoundException("User not found.");
        return user;
    }
}
