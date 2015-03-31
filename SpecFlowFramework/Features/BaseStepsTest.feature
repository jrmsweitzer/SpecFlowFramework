Feature: ScenarioRunner
	In order to avoid silly mistakes
	As a tester
	I want to be able to create simple, dynamic tests.

Scenario: Select an option in Heroku
	Given I am at url "http://the-internet.herokuapp.com/"
	When I click link with exact text "Dropdown"
	Then My url should match "http://the-internet.herokuapp.com/dropdown"
	When I select option "Option 1" from Heroku Dropdown
	Then Heroku Dropdown should have "Option 1" selected
	When I select option "Option 2" from Heroku Dropdown
	Then Heroku Dropdown should have "Option 2" selected
	When I select option "Option 1" from Heroku Dropdown
	Then Heroku Dropdown should have "Option 1" selected

Scenario: Check a checkbox in Heroku
	Given I am at url "http://the-internet.herokuapp.com/"
	When I click link with exact text "Checkboxes"
	Then My url should match "http://the-internet.herokuapp.com/checkboxes"
	When I click on Unchecked Checkbox
	Then Unchecked Checkbox should be checked
	When I click on Unchecked Checkbox
	Then Unchecked Checkbox should be unchecked
	When I click on Unchecked Checkbox
	Then Unchecked Checkbox should be checked

Scenario: Click a link containing text
	Given I am at url "http://the-internet.herokuapp.com/"
	When I click link containing text "Shifting"
	Then My url should match "http://the-internet.herokuapp.com/shifting_content"

Scenario: There should be no broken images
	Given I am at url "http://the-internet.herokuapp.com/broken_images"
	Then I should have no broken images

Scenario: ByFormatter Test 1
	Given I am at url "http://the-internet.herokuapp.com/challenging_dom"
	Then The text in table Table 1 in row 1 column 1 should be "Iuvaret0"
	And The text in table Table 1 in row 4 column 5 should be "Consequuntur3"
	And The text in table Table 1 in row 7 column 3 should be "Adipisci6"
	And Table Table 1 should have 10 rows
	And Table Table 1 should have 7 columns

Scenario: ByFormatter Test 2
	Given I am at url "http://the-internet.herokuapp.com/large"
	Then Table Table 2 should have 50 rows
	And Table Table 2 should have 50 columns
	And The text in table Table 2 in row 27 column 38 should be "27.38"
	And The text in table Table 2 in row 13 column 29 should be "13.29"
	And The text in table Table 2 in row 14 column 15 should be "14.15"
	And The text in table Table 2 in row 45 column 5 should be "45.5"

