using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    public async Task<IEnumerable<Post>> GetPosts()
    {
        var posts = Enumerable.Range(1, 10).Select(x => new Post()
        {
            PostId = x,
            Description = $"Description {x}",
            Date = DateTime.Now,
            Image = $"https//myapis.com/{x}",
            UserId = x * 2,
        });

        await Task.Delay(10);
        return posts;
    }
}