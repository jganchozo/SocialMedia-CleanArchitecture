using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly SocialMediaContext _context;
    protected readonly DbSet<T> Entities;

    public Repository(SocialMediaContext context)
    {
        _context = context;
        Entities = context.Set<T>();
    }
    
    public IEnumerable<T> GetAll()
    {
        return Entities.AsEnumerable();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await Entities.FindAsync(id);
    }

    public async Task Add(T entity)
    {
        await Entities.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Entities.Update(entity);
    }
    
    public void Delete(T entity)
    {
        Entities.Remove(entity);
    }
}