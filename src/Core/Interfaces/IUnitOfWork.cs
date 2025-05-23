using Core.Entities;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPostRepository PostRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Comment> CommentRepository { get; }
    void SaveChanges();
    Task SaveChangesAsync();
}