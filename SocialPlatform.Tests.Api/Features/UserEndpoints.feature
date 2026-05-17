Feature: User Login

    Scenario: Login with credentials
        When I login with email test@email.com and password Password!@#4
        Then login should be successful

    Scenario: Admin fetch all pending users
        Given authenticated user
        When fetch pending users
        Then expected number of pending users should be returned
    
    @CleanUp-DeleteUser
    Scenario: Register new user
        When register
        Then registration should be successful
        Then user has correctly saved details