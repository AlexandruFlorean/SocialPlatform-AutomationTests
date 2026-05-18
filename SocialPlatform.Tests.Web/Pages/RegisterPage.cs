using Microsoft.Playwright;

namespace SocialPlatform.Tests.Web.Pages;

public class RegisterPage(IPage page) : BasePage(page)
{
    private ILocator FirstNameInput => Page.Locator("#firstName");
    private ILocator LastNameInput => Page.Locator("#lastName");
    private ILocator EmailInput => Page.Locator("#email");
    private ILocator PasswordInput => Page.Locator("#password");
    private ILocator ConfirmPasswordInput => Page.Locator("#confirmPassword");
    private ILocator RegisterButton => Page.Locator("button[type='submit']");

    public async Task RegisterAsync(string firstName, string lastName, string email, string password)
    {
        await FirstNameInput.FillAsync(firstName);
        await LastNameInput.FillAsync(lastName);
        await EmailInput.FillAsync(email);
        await PasswordInput.FillAsync(password);
        await ConfirmPasswordInput.FillAsync(password);
        await RegisterButton.ClickAsync();
    }
}
