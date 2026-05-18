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

    Scenario: Register new user with existing email
        Given existing user with email
        Then the response status code should be "Conflict"
        And the response message should be "Email address already registered"

    Scenario Outline: Register fails when password does not meet requirments
        When I attempt to register with password "<Password>"
        Then the response status code should be "BadRequest"
        And the response message should be "[password must contain at least one letter, one number, one special character, and have a minimum length of 8]" 
