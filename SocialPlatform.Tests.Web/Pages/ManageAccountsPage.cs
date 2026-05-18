using Microsoft.Playwright;

namespace SocialPlatform.Tests.Web.Pages;

public class ManageAccountsPage(IPage page) : BasePage(page)
{
    public ILocator Title => Page.Locator("h2:text('Manage User Accounts')");
}
