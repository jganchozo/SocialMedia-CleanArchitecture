using Core.Extensions;
using Core.QueryFilters;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class UriService : IUriService
{
    private readonly string _baseUri;

    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }

    public Uri? GetPostPaginationUri(PostQueryFilter filter, int? page, string actionUrl)
    {
        var baseUrl = $"{_baseUri}{actionUrl}";

        if (page is null) return null;

        baseUrl = baseUrl
            .FilterByPageNumber(page)
            .FilterByPageSize(filter.PageSize)
            .FilterByDate(filter.Date)
            .FilterByDescription(filter.Description);

        return new Uri(baseUrl);
    }
}