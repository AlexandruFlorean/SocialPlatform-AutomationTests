using System.Text.Json.Serialization;

namespace SocialPlatform.Tests.Common.Models.Requests;

public class RegisterRequest
{
    [JsonPropertyName("firstName")]
    public required string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public required string LastName { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
    [JsonPropertyName("publicContent")]
    public required bool PublicContent { get; set; }
}
