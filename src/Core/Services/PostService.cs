using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class PostService(IPostRepository postRepository, IUserRepository userRepository) : IPostService
{
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await postRepository.GetPosts();
    }

    public async Task<Post?> GetPost(int id)
    {
        return await postRepository.GetPost(id);
    }

    public async Task InsertPost(Post post)
    {
        var user = await userRepository.GetUser(post.UserId);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (post.Description.Contains("sex", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Content not allowed");
        }
        
        await postRepository.InsertPost(post);
    }

    public async Task<bool> UpdatePost(Post post)
    {
        return await postRepository.UpdatePost(post);
    }

    public async Task<bool> DeletePost(int id)
    {
        return await postRepository.DeletePost(id);
    }
}