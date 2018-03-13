Feature: AgeRanger UI Tests
	This feature will cover the different tests for Age Ranger Application



@AgeRangerUIRegression
Scenario Outline: Add New Person and Verify Age Group
	Given I am on Age Ranger Home Page
	When I click Add New Person button
	And I enter <FirstName>, <LastName> and <Age> in the form
	And I Submit the form
	And I confirm the action
	Then I should see <FirstName> and <LastName> in the People view with correct <Age> and the correct <AgeGroup>
	And I delete created person <FirstName>, <LastName> and <Age>

Examples:
		|FirstName			|LastName			|Age	|AgeGroup		|
		|ToddlerFirst		|ToddlerLast		|0 		|Toddler   		|
		|ChildFirst			|ChildLast			|2		|Child   		|
		|TeenagerFirst		|TeenagerLast		|14 	|Teenager   	|
		|EarlyTwentiesFirst	|EarlyTwentiesLast	|20 	|Early twenties |
		|AlmostThirtyFirst	|AlmostThirtyLast	|25 	|Almost thirty  |
		|VeryAdultFirst		|VeryAdultLast		|30 	|Very adult  	|
		|KindaOldFirst		|KindaOldLast		|50 	|Kinda old  	|
		|OldFirst			|OldLast			|70 	|Old  			|
		|VeryOldFirst		|VeryOldLast		|99 	|Very old  		|
		|CrazyAncientFirst	|CrazyAncientLast	|110 	|Crazy ancient  |
		|VampireFirst		|VampireLast		|199 	|Vampire  		|
		|KauriTreeFirst		|KauriTreeLast		|4999 	|Kauri tree  	|




@AgeRangerUIRegression
Scenario Outline: Edit Person
	Given I created a new person with <FirstName>, <LastName> and <Age>
	When I update the <FirstName>, <LastName> and <Age> with <NewFirstName>, <NewLastName> and <NewAge>
	And I Submit the form
	And I confirm the action
	Then I should see <NewFirstName> and <NewLastName> in the People view with correct <NewAge> instead of <FirstName>, <LastName> and <Age>
	And I delete updated person <NewFirstName>, <NewLastName> andd <NewAge>
	
Examples:
		|FirstName					|LastName				|Age	|NewFirstName			|NewLastName			|NewAge		|
		|FNOnlyEditingUserFirst		|FNOnlyEditingUserLast	|30		|FNOnlyEditedUserFirst  |NoChange				|NoChange	|
		|LNOnlyEditingUserFirst		|LNOnlyEditingUserLast	|30		|NoChange  				|LNOnlyEditedUserLast	|NoChange	|
		|AgeOnlyEditingUserFirst	|AgeOnlyEditingUserLast	|30		|NoChange  				|NoChange				|50			|
		|AllEditingUserFirst		|AllEditingUserLast		|30		|AllEditedUserFirst 	|AllEditedUserLast		|50			|




@AgeRangerUIRegression
Scenario Outline: Delete Person
	Given I created a new person with <FirstName>, <LastName> and <Age>
	When I delete created person <FirstName>, <LastName> and <Age>
	Then I should not see <FirstName>, <LastName> and <Age> record anymore
	
Examples:
		|FirstName			|LastName			|Age	|
		|DeletingUserFirst	|DeletingUserLast	|30 	|