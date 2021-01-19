Feature: Countries	

Scenario: Add a country
	Given I have the endpoint ""	
	When I add a new country 
	Then Status code "405" is returned
	And Error message "Method Not Allowed" is returned

Scenario: Get number of countries containing united in their name
	Given I have the endpoint "name/united"	
	When I perform a get call
	Then the result contains more than one country
	And the result contains the capital "Dodoma"

Scenario Outline: Get the capital name of the a country
	Given I have the endpoint <countryName>	
	When I perform a get call
	Then the result contains the capital <capitalName>

	Examples: 
	| countryName		                                            | capitalName        |
	| "name/United States of America"	                            | "Washington, D.C." |
	| "name/Tanzania, United Republic of"	                        | "Dodoma"           |
	| "name/United Kingdom of Great Britain and Northern Ireland"	| "London"           |                                




