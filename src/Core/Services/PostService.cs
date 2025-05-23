using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.QueryFilters;
using Core.Extensions;

namespace Core.Services;

public class PostService(IUnitOfWork unitOfWork) : IPostService
{
    public IEnumerable<Post> GetPosts(PostQueryFilter filters)
    {
        var posts = unitOfWork.PostRepository.GetAll();

        posts = posts
            .FilterByUserId(filters.UserId)
            .FilterByDate(filters.Date)
            .FilterByDescription(filters.Description);

        return posts;
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
            throw new BusinessException("User not found");
        }
        
        var userPosts = await unitOfWork.PostRepository.GetPostsByUser(post.UserId);

        var posts = userPosts.ToList();
        if (posts.Count < 10)
        {
            var lastPost = posts.OrderByDescending(x => x.Date).FirstOrDefault();
            if (lastPost is not null && (DateTime.Now - lastPost.Date).TotalDays < 7)
            {
                throw new BusinessException("You are not allowed to add a post");
            }
        }

        if (post.Description.Contains("sex", StringComparison.CurrentCultureIgnoreCase))
        {
            throw new BusinessException("Content not allowed");
        }

        await unitOfWork.PostRepository.Add(post);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdatePost(Post post)
    {
        unitOfWork.PostRepository.Update(post);
        await unitOfWork.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeletePost(int id)
    {
        var post = await unitOfWork.PostRepository.GetById(id);
        
        if (post is null) return false;
        
        unitOfWork.PostRepository.Delete(post);
        await unitOfWork.SaveChangesAsync();
        return true;
    }
}
