using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class PostService(IRepository<Post> postRepository, IRepository<User> userRepository) : IPostService
{
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await postRepository.GetAll();
    }

    public async Task<Post?> GetPost(int id)
    {
        return await postRepository.GetById(id);
    }

    public async Task InsertPost(Post post)
    {
        var user = await userRepository.GetById(post.UserId);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (post.Description.Contains("sex", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Content not allowed");
        }
        
        await postRepository.Add(post);
    }

    public async Task<bool> UpdatePost(Post post)
    {
        await postRepository.Update(post);
        return true;
    }

    public async Task<bool> DeletePost(int id)
    {
        await postRepository.Delete(id);
        return true;
    }
}