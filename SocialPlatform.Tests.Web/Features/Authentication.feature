Feature: Authentication

  @CleanupUser
  Scenario: Successful registration redirects to login page
    Given I am on the register page
    When I register with valid details
    Then I should be redirected to the login page

  Scenario: Successful admin login navigates to manage accounts page
    Given I have a registered admin user
    And I am on the login page
    When I login with valid admin credentials
    Then I should be navigated to the manage accounts page
