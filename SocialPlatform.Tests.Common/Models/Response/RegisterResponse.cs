using SocialPlatform.Tests.Common.Enums;
using System.Text.Json.Serialization;

namespace SocialPlatform.Tests.Common.Models.Response;

public class RegisterResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;
    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("role")]
    public Role Role { get; set; }
    [JsonPropertyName("publicContent")]
    public bool PublicContent { get; set; }
}
