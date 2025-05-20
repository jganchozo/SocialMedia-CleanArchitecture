using Core.Entities;
using Core.Interfaces;

namespace Core.Services;

public class PostService(IUnitOfWork unitOfWork) : IPostService
{
    public async Task<IEnumerable<Post>> GetPosts()
    {
        return await unitOfWork.PostRepository.GetAll();
    }

    public async Task<Post?> GetPost(int id)
    {
        return await unitOfWork.PostRepository.GetById(id);
    }

    public async Task InsertPost(Post post)
    {
        var user = await unitOfWork.UserRepository.GetById(post.UserId);

        if (user is null)
        {
            throw new Exception("User not found");
        }

        if (post.Description.Contains("sex", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new Exception("Content not allowed");
        }

        await unitOfWork.PostRepository.Add(post);
    }

    public async Task<bool> UpdatePost(Post post)
    {
        await unitOfWork.PostRepository.Update(post);
        return true;
    }

    public async Task<bool> DeletePost(int id)
    {
        var post = await unitOfWork.PostRepository.GetById(id);
        
        if (post is null) return false;
        
        await unitOfWork.PostRepository.Delete(post);
        return true;
    }
}
