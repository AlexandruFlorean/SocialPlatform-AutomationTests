using System.Text.Json.Serialization;

namespace SocialPlatform.Tests.Common.Models.Requests;

public class LoginRequest
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
}