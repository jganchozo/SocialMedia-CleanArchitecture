using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class UnitOfWork(SocialMediaContext context) : IUnitOfWork
{
    private IPostRepository? _postRepository;
    private IRepository<User>? _userRepository;
    private Repository<Comment>? _commentRepository;

    public void Dispose()
    {
        context.Dispose();
        GC.SuppressFinalize(this);
    }

    public IPostRepository PostRepository => _postRepository ??= new PostRepository(context);
    public IRepository<User> UserRepository => _userRepository ??= new Repository<User>(context);
    public IRepository<Comment> CommentRepository => _commentRepository ??= new Repository<Comment>(context);

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}