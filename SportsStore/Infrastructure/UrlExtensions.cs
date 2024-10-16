namespace SportsStore.Infrastructure;

public static class UrlExtensions
{
    public static string PathAndQuery(this HttpRequest request)
    {
        if (request is null)
        {
            return string.Empty;
        }

        return request.QueryString.HasValue ? $"{request.Path}{request.QueryString}" : request.Path.ToString();
    }
}
