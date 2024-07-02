using Core.Entities;

namespace Core.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPosts();
}