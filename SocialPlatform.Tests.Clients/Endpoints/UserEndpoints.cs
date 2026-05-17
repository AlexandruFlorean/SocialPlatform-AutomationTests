using Reqnroll;
using SocialPlatform.Tests.Clients.Base;
using SocialPlatform.Tests.Common.Models.Requests;
using SocialPlatform.Tests.Common.Settings;

namespace SocialPlatform.Tests.Clients.Endpoints;

public class UserEndpoints(ApiClient apiClient, ApiSettings apiSettings, ScenarioContext scenarioContext) : BaseEndpoint(scenarioContext)
{
    public async Task<ApiBaseResponse> LoginAsync(LoginRequest request)
    {
        var url = apiSettings.Urls.Login;
        return await apiClient.PostAsync(url, request);
    }

    public async Task<ApiBaseResponse> RegisterAsync(RegisterRequest request)
    {
        var url = apiSettings.Urls.Register;
        return await apiClient.PostAsync(url, request);
    }

    public async Task<ApiBaseResponse> PendingUsersAsync()
    {
        var authHeader = GetAuthHeader();
        var url = apiSettings.Urls.PendingUsers;
        return await apiClient.GetAsync(url, authHeader);
    }
}