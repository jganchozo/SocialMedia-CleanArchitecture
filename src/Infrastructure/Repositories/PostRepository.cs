using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository(SocialMediaContext context) : Repository<Post>(context), IPostRepository
{
    public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
    {
        return await Entities.Where(x => x.UserId == userId).ToListAsync();
    }
}