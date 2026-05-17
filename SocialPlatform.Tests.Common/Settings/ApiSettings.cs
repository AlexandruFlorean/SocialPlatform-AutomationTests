namespace SocialPlatform.Tests.Common.Settings;

public class ApiSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = string.Empty;
    public required ApiUrls Urls { get; set; } 
}
