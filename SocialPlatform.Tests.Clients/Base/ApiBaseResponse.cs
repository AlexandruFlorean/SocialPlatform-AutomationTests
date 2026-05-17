using Microsoft.Playwright;
using Newtonsoft.Json;
using System.Net;

namespace SocialPlatform.Tests.Clients.Base;

public class ApiBaseResponse(IAPIResponse apiResponse)
{
    private readonly IAPIResponse apiResponse = apiResponse;

    public HttpStatusCode StatusCode => (HttpStatusCode)apiResponse.Status;

    public string Url => apiResponse.Url;

    //public string ResponseAsText => apiResponse.TextAsync().Result;

    public T Deserialize<T>()
    {
        var responseContent = apiResponse.TextAsync().GetAwaiter().GetResult();
        return JsonConvert.DeserializeObject<T>(responseContent)!;
    }
}