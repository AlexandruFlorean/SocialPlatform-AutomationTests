using SocialPlatform.Tests.Common.Enums;
using System.Text.Json.Serialization;

namespace SocialPlatform.Tests.Common.Models.Response;

public class LoginResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = string.Empty;
    [JsonPropertyName("userId")]
    public Guid UserId { get; set; }
    [JsonPropertyName("role")]
    public Role Role { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

