using Reqnroll;
using SocialPlatform.Tests.Common.Constants;

namespace SocialPlatform.Tests.Clients.Endpoints;
public abstract class BaseEndpoint(ScenarioContext scenarioContext)
{
    /// <summary>
    /// Generates the Authorization header dictionary if a token exists in the test context.
    /// </summary>
    protected Dictionary<string, string>? GetAuthHeader()
    {
        if (scenarioContext.ContainsKey(ScenarioContextKeys.Token))
        {
            var token = scenarioContext.Get<string>(ScenarioContextKeys.Token);
            return new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}" }
            };
        }

        return null;
    }
}