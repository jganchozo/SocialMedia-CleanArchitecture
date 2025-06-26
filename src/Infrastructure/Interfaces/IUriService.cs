using Core.QueryFilters;

namespace Infrastructure.Interfaces;

public interface IUriService
{
    Uri? GetPostPaginationUri(PostQueryFilter filter, int? page, string actionUrl);
}