using Core.Entities;

namespace Core.Interfaces;

public interface IPostRepository
{
    Task<IEnumerable<Publicacion>> GetPosts();
}