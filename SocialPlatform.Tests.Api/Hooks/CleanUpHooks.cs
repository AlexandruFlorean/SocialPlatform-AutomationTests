using Microsoft.EntityFrameworkCore;
using Reqnroll;
using SocialPlatform.Tests.Clients.Base;
using SocialPlatform.Tests.Common.Constants;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Models.Response;

namespace SocialPlatform.Tests.Api.Hooks;

[Binding]
public class CleanUpHooks(SocialPlatformDbContext dbContext,
    ScenarioContext scenarioContext)
{
    [AfterScenario("CleanUp-DeleteUser")]
    public async Task DeleteUserAsync()
    {
        var apiResponse = scenarioContext.Get<ApiBaseResponse>(ScenarioContextKeys.ApiResponse);
        var userResponse = apiResponse.Deserialize<BaseResponse<RegisterResponse>>();
        var userId = userResponse.Response.Id;
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) { return; }

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
    }
}
