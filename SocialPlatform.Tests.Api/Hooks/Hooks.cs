using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;
using SocialPlatform.Tests.Clients.Base;
using SocialPlatform.Tests.Clients.Clients;
using SocialPlatform.Tests.Clients.Endpoints;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Settings;

namespace SocialPlatform.Tests.Api.Hooks;

[Binding]
public class Hooks(IObjectContainer objectContainer)
{
    private static IPlaywright _playwright = null!;
    private static IAPIRequestContext _requestContext = null!;
    private static ApiSettings _apiSettings= null!;

    private readonly IObjectContainer _container = objectContainer;

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        await InitializeDependenciesBeforeTestsRunAsync();
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await RegisterDependenciesBeforeScenarioAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun() 
    {
        await DisposeDependenciesAfterTestsRunAsync();
    }

    private static async Task InitializeDependenciesBeforeTestsRunAsync()
    {
        var config = new ConfigurationBuilder()
       .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
       .AddJsonFile("appsettings.json")
       .Build();
        _apiSettings = config.GetSection("ApiSettings").Get<ApiSettings>()!;

        _playwright = await Playwright.CreateAsync();

        _requestContext = await _playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = _apiSettings.BaseUrl,
            IgnoreHTTPSErrors = true
        });
    }

    private async Task RegisterDependenciesBeforeScenarioAsync()
    {
        _container.RegisterInstanceAs(_requestContext);
        _container.RegisterInstanceAs(_apiSettings);

        _container.RegisterTypeAs<ApiClient, ApiClient>();
        _container.RegisterTypeAs<UserEndpoints, UserEndpoints>();
        _container.RegisterTypeAs<SocialPlatformApiClient, SocialPlatformApiClient>();

        var connectionString = _apiSettings.ConnectionString;
        var options = new DbContextOptionsBuilder<SocialPlatformDbContext>()
            .UseSqlServer(connectionString)
            .Options;
        var dbContext = new SocialPlatformDbContext(options);

        _container.RegisterInstanceAs(dbContext);
    }

    private static async Task DisposeDependenciesAfterTestsRunAsync()
    {
        if (_requestContext != null) await _requestContext.DisposeAsync();
        _playwright?.Dispose();
    }
}
