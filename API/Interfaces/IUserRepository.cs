using API.Models;

namespace API.Repositories.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    Task<bool> SaveAllAsync();
    Task<User?> GetUserByUsernameAsync(string username);
    Task<bool> UserExistsAsync(string username, string email);
}
