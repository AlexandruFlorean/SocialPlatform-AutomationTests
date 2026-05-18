using Reqnroll;
using SocialPlatform.Tests.Common.Constants;
using SocialPlatform.Tests.Common.DatabaseContext;

namespace SocialPlatform.Tests.Web.Hooks;

[Binding]
public class CleanUpHooks(SocialPlatformDbContext dbContext, ScenarioContext scenarioContext)
{
    [AfterScenario("CleanupUser")]
    public async Task AfterScenario()
    {
        var testEmail = scenarioContext.Get<string>(ScenarioContextKeys.UserEmail);
        dbContext.Remove(dbContext.Users.Single(u => u.Email == testEmail));
        await dbContext.SaveChangesAsync();
    }
}
