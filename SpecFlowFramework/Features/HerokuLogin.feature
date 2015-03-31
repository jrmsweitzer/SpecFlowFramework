Feature: HerokuLogin
	In order to avoid login issues
	As a user
	I want the login page to work as expected.

Scenario: Valid credentials should log in successfully
	Given I am at url "http://the-internet.herokuapp.com/login"
	When I input "tomsmith" into Login Username
	And I input "SuperSecretPassword!" into Login Password
	And I click on Login Button
	Then I should log in successfully

Scenario: Incorrect username should fail login
	Given I am at url "http://the-internet.herokuapp.com/login"
	When I input "Invalid Username" into Login Username
	And I click on Login Button
	Then I should not log in successfully
	And There should be an error message on the screen
	And The error message should say "Your username is invalid!"

Scenario: Incorrect password should fail login
	Given I am at url "http://the-internet.herokuapp.com/login"
	When I input "tomsmith" into Login Username
	And I input "Invalid Password" into Login Password
	And I click on Login Button
	Then I should not log in successfully
	And There should be an error message on the screen
	And The error message should say "Your password is invalid!"

Scenario: Skipping login page should fail login
	Given I am at url "http://the-internet.herokuapp.com/login"
	When I try to go directly to the secure area
	Then I should not log in successfully
	And There should be an error message on the screen
	And The error message should say "You must login to view the secure area!"