using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly SocialMediaContext _context;
    private readonly DbSet<T> _entities;

    public Repository(SocialMediaContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }
    
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await GetById(id);
        
        if (entity is null) return;
        
        _entities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}