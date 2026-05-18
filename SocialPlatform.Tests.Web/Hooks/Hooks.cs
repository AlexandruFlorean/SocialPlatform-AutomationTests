using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Settings;
using SocialPlatform.Tests.Web.Pages;

namespace SocialPlatform.Tests.Web.Hooks;

[Binding]
public class Hooks(IObjectContainer objectContainer)
{
    private static IPlaywright _playwright = null!;
    private static IBrowser _browser = null!;
    private static WebSettings _webSettings = null!;

    private readonly IObjectContainer _container = objectContainer;
    private IBrowserContext _context = null!;
    private IPage _page = null!;

    [BeforeTestRun]
    public static async Task BeforeTestRun()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        _webSettings = config.GetSection("WebSettings").Get<WebSettings>()!;

        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false // Set to false for debugging
        });
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();

        _container.RegisterInstanceAs(_webSettings);
        _container.RegisterInstanceAs(_page);

        // Register Pages
        _container.RegisterTypeAs<RegisterPage, RegisterPage>();
        _container.RegisterTypeAs<LoginPage, LoginPage>();
        _container.RegisterTypeAs<ManageAccountsPage, ManageAccountsPage>();

        // Register Database Context
        var options = new DbContextOptionsBuilder<SocialPlatformDbContext>()
            .UseSqlServer(_webSettings.ConnectionString)
            .Options;
        var dbContext = new SocialPlatformDbContext(options);
        _container.RegisterInstanceAs(dbContext);
    }

    [AfterScenario]
    public async Task AfterScenario()
    {
        await _page.CloseAsync();
        await _context.CloseAsync();
    }

    [AfterTestRun]
    public static async Task AfterTestRun()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
