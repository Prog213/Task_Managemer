using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(TaskManagementDbContext context) : IUserRepository
{
    public void AddUser(User user)
    {
        context.Users.Add(user);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UserExistsAsync(string username, string email)
    {
        return await context.Users.AnyAsync(u => u.Username == username)
            || await context.Users.AnyAsync(u => u.Email == email);
    }
}
