using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(SocialMediaContext context) : IUserRepository
{
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUser(int id)
    {
        return await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
    }
}