using Core.Entities;

namespace Core.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAll();
    Task<List<T>> GetAllAsync();
    Task<T?> GetById(int id);
    Task Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}