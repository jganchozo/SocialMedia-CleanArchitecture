namespace Core.Extensions;

public static class UriServiceFilters
{
    private const string QuestionMark = "?";
    private const string Ampersand = "&";

    public static string FilterByPageNumber(this string baseUrl, int? pageNumber)
    {
        if (IsNullOrEmpty(pageNumber)) return baseUrl;

        var separator = ValidateCharacter(baseUrl, QuestionMark);
        return $"{baseUrl}{separator}{nameof(pageNumber)}={pageNumber}";
    }

    public static string FilterByPageSize(this string baseUrl, int? pageSize)
    {
        if (IsNullOrEmpty(pageSize)) return baseUrl;

        var separator = ValidateCharacter(baseUrl, QuestionMark);
        return $"{baseUrl}{separator}{nameof(pageSize)}={pageSize}";
    }

    public static string FilterByDate(this string baseUrl, DateTime? date)
    {
        if (IsNullOrEmpty(baseUrl) || IsNullOrEmpty(date)) return baseUrl;

        var separator = ValidateCharacter(baseUrl, QuestionMark);
        var encodedDate = Uri.EscapeDataString(date!.Value.ToString("yyyy-MM-dd"));

        return $"{baseUrl}{separator}{nameof(date)}={encodedDate}";
    }

    public static string FilterByDescription(this string baseUrl, string? description)
    {
        if (IsNullOrEmpty(description)) return baseUrl;

        var separator = ValidateCharacter(baseUrl, QuestionMark);
        var encodedDescription = Uri.EscapeDataString(description!);

        return $"{baseUrl}{separator}{nameof(description)}={encodedDescription}";
    }

    private static bool IsNullOrEmpty<T>(T value) => value is null || string.IsNullOrEmpty(value.ToString());

    private static string ValidateCharacter(string url, string character) =>
        url.Contains(character) ? Ampersand : QuestionMark;
}