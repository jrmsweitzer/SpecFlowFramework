Feature: Cart Total
	As a shopper
	I want to see my cart total on every screen
	So I don't have to leave my current page to verify it's contents

Background: 
	Given I am at url "http://store.demoqa.com/"

Scenario: Empty Cart
	Then My cart should have a total of 0


Scenario: Add an item
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 1


Scenario: Add same item twice, separately
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 2


Scenario: Add two separate items
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	And I click link with exact text "Home"
	And I click link with exact text "Buy Now"
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 2


Scenario: Variable test
	When I click link with exact text "All Product"

	And I store text of Product Title into Product 1
	Then Variable Product 1 should be "iPhone 5"
	When I store text of Product Price into Product 1 Price
	Then Variable Product 1 Price should be "$12.00"

	When I Add To Cart
	And I Go To Checkout

	Then Product 1 should exist in Table Product Checkout Table
	When I store row number containing Product 1 from Table Product Checkout Table into Product 1 Row
	Then The text of Table Product Checkout Table Row Product 1 Row Column 4 should be Product 1 Price

	When I continue checkout
	Then Text of Item Cost should be Product 1 Price
	When I fill out the user form
	And I click on Purchase

	Then Table Purchase Results Table should have 1 rows
	And Product 1 should exist in Table Purchase Results Table
	And The text of Table Purchase Results Table Row 1 Column 2 should be Product 1 Price

	# This is a comment
Scenario: Search For Product
	When I input "Apple TV" into Search Bar
	And Submit Search Bar

	Then Text of Prod Title should be Apple TV
	And Text of Current Price should be $80.00

	When I Add To Cart
	And I Go To Checkout

	Then Apple TV should exist in Table Product Checkout Table
	And The text of Table Product Checkout Table Row 2 Column 4 should be $80.00

	When I continue checkout
	Then Text of Item Cost should be $80.00
	When I fill out the user form
	And I click on Purchase

	Then Table Purchase Results Table should have 1 rows
	And Apple TV should exist in Table Purchase Results Table
	And The text of Table Purchase Results Table Row 1 Column 2 should be $80.00


Scenario: Remove an item from the cart