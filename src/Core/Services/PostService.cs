using Core.CustomEntities;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces;
using Core.QueryFilters;
using Core.Extensions;
using Microsoft.Extensions.Options;

namespace Core.Services;

public class PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options) : IPostService
{
    private readonly PaginationOptions _paginationOptions = options.Value;

    public PagedList<Post> GetPosts(PostQueryFilter filters)
    {
        filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
        filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

        var posts = unitOfWork.PostRepository.GetAll();

        posts = posts
            .FilterByUserId(filters.UserId)
            .FilterByDate(filters.Date)
            .FilterByDescription(filters.Description);

        var pagedPosts = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);

        return pagedPosts;
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
