using Core.CustomEntities;
using Core.Entities;
using Core.QueryFilters;

namespace Core.Interfaces;

public interface IPostService
{
    PagedList<Post> GetPosts(PostQueryFilter filters);
    Task<Post?> GetPost(int id);
    Task InsertPost(Post post);
    Task<bool> UpdatePost(Post post);
    Task<bool> DeletePost(int id);
}