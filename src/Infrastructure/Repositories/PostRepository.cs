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

    public async Task<bool> UpdatePost(Post post)
    {
        var currentPost = await GetPost(post.PostId);
        
        if (currentPost is null) return false;
        
        currentPost.Date = post.Date;
        currentPost.Description = post.Description;
        currentPost.Image = post.Image;

        int rows = await context.SaveChangesAsync();
        return rows > 0;
    }
    
    public async Task<bool> DeletePost(int id)
    {
        var currentPost = await GetPost(id);
        
        if (currentPost is null) return false;
        
        context.Posts.Remove(currentPost);

        int rows = await context.SaveChangesAsync();
        return rows > 0;
    }
}