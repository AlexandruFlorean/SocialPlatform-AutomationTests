using System.Text.Json.Serialization;

namespace SocialPlatform.Tests.Common.Models.Response;

public class BaseResponse<T>
{
    [JsonPropertyName("response")]
    public required T Response { get; set; }
    [JsonPropertyName("isError")]
    public bool IsError { get; set; }
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}
