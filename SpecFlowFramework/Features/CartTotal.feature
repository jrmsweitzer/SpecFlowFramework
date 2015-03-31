Feature: Cart Total
	As a shopper
	I want to see my cart total on every screen
	So I don't have to leave my current page to verify it's contents

Scenario: Empty Cart
	Given I am at url "http://store.demoqa.com/"
	Then My cart should have a total of 0

Scenario: Add an item
	Given I am at url "http://store.demoqa.com/"
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 1

Scenario: Add same item twice, separately
	Given I am at url "http://store.demoqa.com/"
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 2

Scenario: Add two separate items
	Given I am at url "http://store.demoqa.com/"
	When I click link with exact text "All Product"
	And I Add To Cart
	And I Continue Shopping
	And I click link with exact text "Home"
	And I click link with exact text "Buy Now"
	And I Add To Cart
	And I Continue Shopping
	Then My cart should have a total of 2

Scenario: Variable test
	Given I am at url "http://store.demoqa.com/"
	When I click link with exact text "All Product"
	And I store text of Product Title into variable Product 1
	Then Variable Product 1 should be "iPhone 5"
	When I store text of Product Price into variable Product 1 Price
	Then Variable Product 1 Price should be "$12.00"
	When I Add To Cart
	And I Go To Checkout
	Then Variable Product 1 should exist in Table Product Checkout Table
	When I store row number containing variable Product 1 from Table Product Checkout Table into variable Product 1 Row
	Then The text of Table Product Checkout Table Row Product 1 Row Column 4 should be Product 1 Price
	When I continue checkout
	And I input "testaccountcatalyst" into Store username
	And I input "Password13" into Store password
	And I click on Store login button
	Then Text of Item Cost should be Product 1 Price