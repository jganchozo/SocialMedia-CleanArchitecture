using Core.Entities;

namespace Core.Extensions;

public static class PostFilters
{
    public static IQueryable<Post> FilterByUserId(this IQueryable<Post> query, int? userId)
    {
        return userId.HasValue ? query.Where(x => x.UserId == userId) : query;
    }

    public static IQueryable<Post> FilterByDate(this IQueryable<Post> query, DateTime? date)
    {
        return date.HasValue ? query.Where(x => x.Date.Date == date.Value.Date) : query;
    }

    public static IQueryable<Post> FilterByDescription(this IQueryable<Post> query, string? description)
    {
        return !string.IsNullOrEmpty(description)
            ? query.Where(x => x.Description.Contains(description))
            : query;
    }
}