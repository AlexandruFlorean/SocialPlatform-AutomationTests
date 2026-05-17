using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reqnroll;
using SocialPlatform.Tests.Clients.Base;
using SocialPlatform.Tests.Clients.Clients;
using SocialPlatform.Tests.Common.Constants;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Models.Requests;
using SocialPlatform.Tests.Common.Models.Response;
using System.Net;

namespace SocialPlatform.Tests.Api.StepDefinitions;

[Binding]
public class UserEndPointsStepDefinition(
    SocialPlatformApiClient client, 
    ScenarioContext scenarioContext,
    SocialPlatformDbContext dbContext)
{
    [Given("authenticated user")]
    public async Task GivenAuthenticatedUser()
    {
        var loginRequest = new LoginRequest
        {
            Email = "test@email.com",
            Password = "Password!@#4"
        };
        var response = await client.UserEndpoints.LoginAsync(loginRequest);
        var loginResponse = response.Deserialize<BaseResponse<LoginResponse>>();
        scenarioContext.Add(ScenarioContextKeys.Token, loginResponse.Response.Token);
    }

    [When("fetch pending users")]
    public async Task WhenFetchPendingUsers()
    {
        var response = await client.UserEndpoints.PendingUsersAsync();
        scenarioContext.Add(ScenarioContextKeys.ApiResponse, response);
    }

    [When("I login with email (.*) and password (.*)")]
    public async Task WhenILogin(string email, string password)
    {
        var request = new LoginRequest
        {
            Email = email,
            Password = password
        };

        var response = await client.UserEndpoints.LoginAsync(request);
        scenarioContext.Add(ScenarioContextKeys.ApiResponse, response);
    }

    [Then("login should be successful")]
    public void ThenLoginShouldBeSuccessful()
    {
        var response = scenarioContext.Get<ApiBaseResponse>(ScenarioContextKeys.ApiResponse)!;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Then("expected number of pending users should be returned")]
    public async Task ThenExpectedNumberOfPendingUsersShouldBeReturnedAsync()
    {
        var response = scenarioContext.Get<ApiBaseResponse>(ScenarioContextKeys.ApiResponse)!;
        Assert.That(response, Is.Not.Null);
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        

        var pendingUsersResponse = response.Deserialize<BaseResponse<List<UserDtoResponse>>>();
        Assert.That(pendingUsersResponse, Is.Not.Null);
        var expectedPendingUsersCount = await dbContext.Users.CountAsync(u => !u.Active);
        var actualPendingUsersCount = pendingUsersResponse.Response.Count;
        Assert.That(actualPendingUsersCount, Is.EqualTo(expectedPendingUsersCount));
    }
}