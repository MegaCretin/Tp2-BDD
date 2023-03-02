Feature: Election Result Calculation
As a client of the API at the end of a majority vote
I want to calculate the result of the vote
So that I can obtain the winner of the vote

	Scenario: Single candidate has more than 50% of votes
	Given the election has closed
	And there are 3 candidates
	And candidate 1 has 51% of the votes
	When I calculate the result of the election
	Then candidate 1 should be declared the winner


	Scenario: No single candidate has more than 50% of votes
	Given the election has closed
	And there are 3 candidates
	And candidate 1 has 49% of the votes
	And candidate 2 has 30% of the votes
	And candidate 3 has 21% of the votes
	When I calculate the result of the election
	Then a second round of election should be held
	And only candidate 1 and 2 should participate in the second round

	Scenario: Second round election results in tie
		Given the second round of election has closed
		And there are 2 candidates
		And both candidates have 50% of the votes each
		When I calculate the result of the second round of election
		Then no winner can be determined

	Scenario: Second round election results in a winner
		Given the second round of election has closed
		And there are 2 candidates
		And candidate 1 has 60% of the votes
		And candidate 2 has 40% of the votes
		When I calculate the result of the second round of election
		Then candidate 1 should be declared the winner 