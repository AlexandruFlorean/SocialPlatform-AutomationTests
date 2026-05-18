using Microsoft.Playwright;

namespace SocialPlatform.Tests.Web.Pages;

public abstract class BasePage(IPage page)
{
    public readonly IPage Page = page;

    public async Task NavigateToAsync(string url) => await Page.GotoAsync(url);

    public async Task WaitForUrlAsync(string url) => await Page.WaitForURLAsync(url);
    public string Url => Page.Url;
}
