using Microsoft.Playwright;

namespace SocialPlatform.Tests.Web.Pages;

public class LoginPage(IPage page) : BasePage(page)
{
    private ILocator EmailInput => Page.Locator("#email");
    private ILocator PasswordInput => Page.Locator("#password");
    private ILocator LoginButton => Page.Locator("button[type='submit']");

    public async Task LoginAsync(string email, string password)
    {
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }
}
