using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reqnroll;
using SocialPlatform.Tests.Clients.Base;
using SocialPlatform.Tests.Clients.Clients;
using SocialPlatform.Tests.Common.Constants;
using SocialPlatform.Tests.Common.DatabaseContext;
using SocialPlatform.Tests.Common.Enums;
using SocialPlatform.Tests.Common.Models.Requests;
using SocialPlatform.Tests.Common.Models.Response;
using System.ComponentModel.DataAnnotations;
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

    [When("register")]
    public async Task Register()
    {
        var registerRequest = new RegisterRequest
        {
            FirstName = "Norbert-Istvan",
            LastName = "Vincze",
            Email = "norbert.vincze@gmail.com",
            Password = "Password!@#4",
            PublicContent = false
        };
        scenarioContext.Add(ScenarioContextKeys.ApiRequest, registerRequest);

        var response = await client.UserEndpoints.RegisterAsync(registerRequest);
        scenarioContext.Add(ScenarioContextKeys.ApiResponse, response);
    }

    [Then("login should be successful")]
    public void ThenLoginShouldBeSuccessful()
    {
        var response = scenarioContext.Get<ApiBaseResponse>(ScenarioContextKeys.ApiResponse)!;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Then("registration should be successful")]
    public void RegistrationShouldBeSuccessful()
    {
        var response = scenarioContext.Get<ApiBaseResponse>(ScenarioContextKeys.ApiResponse)!;
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Then("user has correctly saved details")]
    public async Task RegisterShouldBeSuccessful()
    {
        var request = scenarioContext.Get<RegisterRequest>(ScenarioContextKeys.ApiRequest)!;
        var registeredUser = await dbContext.Users.AsNoTracking().FirstAsync(u => u.Email == request.Email);
        Assert.That(registeredUser, Is.Not.Null);
        Assert.That(registeredUser.Email, Is.Not.Null);
        Assert.That(registeredUser.FirstName, Is.Not.Null);
        Assert.That(registeredUser.FirstName, Is.EqualTo(request.FirstName));
        Assert.That(registeredUser.LastName, Is.Not.Null);
        Assert.That(registeredUser.LastName, Is.EqualTo(request.LastName));
        Assert.That(registeredUser.Password, Is.Not.Null);
        Assert.That(registeredUser.Password, Is.EqualTo(request.Password));
        Assert.That(registeredUser.PublicContent, Is.EqualTo(request.PublicContent));
        Assert.That(registeredUser.Active, Is.False);
        Assert.That(registeredUser.Role, Is.EqualTo((short)Role.Client));
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