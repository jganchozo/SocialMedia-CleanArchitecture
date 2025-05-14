using Core.Entities;

namespace Core.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUser(int id);
}