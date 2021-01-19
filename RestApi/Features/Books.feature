Feature: Books	

Scenario: Get book info
	Given I have the endpoint "isbn/9789000010134" to the books api
	When I perform a get call
	Then the result contains the title "Speak english"

Scenario: Add a book
	Given I have the endpoint ""	
	When I add a new book 
	Then Status code "405" is returned
	And Error message "Method Not Allowed" is returned
