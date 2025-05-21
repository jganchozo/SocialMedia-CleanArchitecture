using Core.Entities;

namespace Core.Interfaces;

public interface IPostRepository : IRepository<Post>
{
    Task<IEnumerable<Post>> GetPostsByUser(int userId);
}