using Core.Entities;
using Core.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly SocialMediaContext _context;
    public PostRepository(SocialMediaContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Publicacion>> GetPosts()
    {
        return await _context.Publicacion.ToListAsync();
    }
}