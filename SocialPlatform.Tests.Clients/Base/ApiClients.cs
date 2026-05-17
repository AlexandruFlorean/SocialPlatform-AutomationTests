using Microsoft.Playwright;

namespace SocialPlatform.Tests.Clients.Base;

public class ApiClient
{
    protected readonly IAPIRequestContext _requestContext;

    public ApiClient(IAPIRequestContext requestContext)
    {
        _requestContext = requestContext;
    }

    public async Task<ApiBaseResponse> GetAsync(string url, Dictionary<string, string>? headers = null)
    {
        var options = MergeHeaders(headers);
        var response = await _requestContext.GetAsync(url, options);
        return new ApiBaseResponse(response);
    }

    public async Task<ApiBaseResponse> PostAsync(string url, object data, Dictionary<string, string>? headers = null)
    {
        var options = MergeHeaders(headers) ?? new APIRequestContextOptions();
        options.DataObject = data;

        var response = await _requestContext.PostAsync(url, options);
        return new ApiBaseResponse(response);
    }

    public async Task<ApiBaseResponse> PutAsync(string url, object data, Dictionary<string, string>? headers = null)
    {
        var options = MergeHeaders(headers) ?? new APIRequestContextOptions();
        options.DataObject = data;

        var response = await _requestContext.PutAsync(url, options);
        return new ApiBaseResponse(response);
    }

    public async Task<ApiBaseResponse> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        var options = MergeHeaders(headers);
        var response = await _requestContext.DeleteAsync(url, options);
        return new ApiBaseResponse(response);
    }

    private APIRequestContextOptions? MergeHeaders(Dictionary<string, string>? headers)
    {
        if (headers == null || headers.Count == 0) return null;

        var options = new APIRequestContextOptions();
        options.Headers = headers;

        return options;
    }
}