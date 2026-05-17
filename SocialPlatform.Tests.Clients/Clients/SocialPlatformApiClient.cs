using SocialPlatform.Tests.Clients.Endpoints;

namespace SocialPlatform.Tests.Clients.Clients;

public class SocialPlatformApiClient(UserEndpoints userEndpoints)
{
    public UserEndpoints UserEndpoints { get; } = userEndpoints;
}
