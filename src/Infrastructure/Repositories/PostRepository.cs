using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository(SocialMediaContext context) : IPostRepository
{
    //private readonly SocialMediaContext _context = context;
    // public PostRepository(SocialMediaContext context)
    // {
    //     _context = context;
    // }
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await context.Posts.ToListAsync();
    }
    
    public async Task<Post?> GetPost(int id)
    {
        return await context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
    }
    
    public async Task InsertPost(Post post)
    {
        context.Posts.Add(post);
        await context.SaveChangesAsync();
    }
}