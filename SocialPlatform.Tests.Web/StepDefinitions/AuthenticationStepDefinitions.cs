using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using Reqnroll;
using SocialPlatform.Tests.Common.Constants;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Enums;
using SocialPlatform.Tests.Common.Settings;
using SocialPlatform.Tests.Web.Pages;
using System.Text.RegularExpressions;

namespace SocialPlatform.Tests.Web.StepDefinitions;

[Binding]
public class AuthenticationStepDefinitions(
    RegisterPage registerPage,
    LoginPage loginPage,
    ManageAccountsPage manageAccountsPage,
    WebSettings webSettings,
    SocialPlatformDbContext dbContext,
    ScenarioContext scenarioContext)
{

    [Given(@"I am on the register page")]
    public async Task GivenIAmOnTheRegisterPage()
    {
        await registerPage.NavigateToAsync($"{webSettings.BaseUrl}/register");
    }

    [When(@"I register with valid details")]
    public async Task WhenIRegisterWithValidDetails()
    {
        string testPassword = "Password123!";
        string testEmail = $"test_{Guid.NewGuid()}@example.com";
        await registerPage.RegisterAsync("Test", "User", testEmail, testPassword);
        scenarioContext.Add(ScenarioContextKeys.UserEmail, testEmail);
    }


    [Then(@"I should be redirected to the login page")]
    public async Task ThenIShouldBeRedirectedToTheLoginPage()
    {
        await Assertions.Expect(registerPage.Page).ToHaveURLAsync(new Regex(".*/login"));
    }

    [Given(@"I have a registered admin user")]
    public async Task GivenIHaveARegisteredUser()
    {
        var adminEmail = "test@email.com";
        var adminPassword = "Password!@#4";
        var existingAdmin = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);
        
        if (existingAdmin is null)
        {
            var user = new User
            {
                FirstName = "Test",
                LastName = "User",
                Email = adminEmail,
                Password = adminPassword,
                Active = true,
                Role = (short)Role.Admin
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }
        
        scenarioContext["UserEmail"] = adminEmail;
        scenarioContext["UserPassword"] = adminPassword;
    }

    [Given(@"I am on the login page")]
    public async Task GivenIAmOnTheLoginPage()
    {
        await loginPage.NavigateToAsync($"{webSettings.BaseUrl}/login");
    }

    [When(@"I login with valid admin credentials")]
    public async Task WhenILoginWithValidCredentials()
    {
        var email = scenarioContext.Get<string>("UserEmail");
        var password = scenarioContext.Get<string>("UserPassword");
        await loginPage.LoginAsync(email, password);
    }

    [Then(@"I should be navigated to the manage accounts page")]
    public async Task ThenIShouldBeNavigatedToTheManageAccountsPage()
    {
        await Assertions.Expect(manageAccountsPage.Title).ToBeVisibleAsync();
        Assert.That(manageAccountsPage.Url, Is.EqualTo($"{webSettings.BaseUrl}/admin"));
    }
}
