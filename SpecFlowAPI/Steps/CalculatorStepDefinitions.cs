using System;
using System.Collections;
using TechTalk.SpecFlow;
using System.Reflection.Metadata.Ecma335;
using FluentAssertions;
using ElectionResultCalculator;

namespace ElectionResultCalculation.Specs
{
    [Binding]
    
    public class ElectionResultCalculationSteps
    {

        private List<int> CandidatsVotes;
        private  int winner;
        private List<ElectionResult.Candidat> _candidats;
        private bool _electionClosed;
        private int _candidatId;
        private int _percentage;
        ElectionResult electionResult = new ElectionResult();

        [Given(@"the election has closed")]
        public void GivenTheElectionHasClosed()
        {
            _electionClosed = true;
        }
        [Given(@"there are (.*) candidates")]
        public void GivenThereAreCandidates(int nbCandidates)
        {
            if (_electionClosed==true)
            {
                CandidatsVotes = electionResult.CreerListCandidats(nbCandidates);
            } 
        }
        [Given(@"candidat (.*) has (.*)% of the votes")]
        public void GivenCandidateHasOfTheVotes(int candidatId, int percentage)
        {
            _candidatId = candidatId;
            _percentage = percentage;
            electionResult.AjouterVotePourCandidat(_candidatId, _percentage);
        }
        [Given(@"the second round of election has closed")]
        public void GivenTheSecondRoundOfElectionHasClosed()
        {
            _electionClosed = true;
        }

        [Given(@"both candidats have (.*)% of the votes each")]
        public void GivenBothCandidatesHaveOfTheVotesEach(int percentage)
        {
            electionResult.CreerListCandidats(2);
            electionResult.AjouterVotePourCandidat(1, percentage);
            electionResult.AjouterVotePourCandidat(2, percentage);
        } 


        [When(@"I calculat the result of the election")]
        public void WhenICalculateTheResultOfTheElection()
        {
            winner= electionResult.CalculerElectionResultat(CandidatsVotes);
        }
        
        [When(@"I calculat the result of the second round of election")]
        public void WhenICalculateTheResultOfTheSecondRoundOfElection()
        {
            winner = electionResult.CalculerElectionResultat(CandidatsVotes);

        }

        [Then(@"candidat (.*) should be declared the winner")]
        public void ThenCandidateShouldBeDeclaredTheWinner(int candidate)
        {
            winner.Should().Be(candidate); 
        }
        [Then(@"a second round of election should be held")]
        public void ThenASecondRoundOfElectionShouldBeHeld()
        {
            electionResult.ShouldHoldSecondRound().Should().BeTrue();
        }

        [Then(@"only candidat (.*) and (.*) should participate in the second round")]
        public void ThenOnlyCandidatAndShouldParticipateInTheSecondRound(int candidat1, int candidat2)
        {
            electionResult.CandidatsInSecondRound().Should().BeEquivalentTo(new List<int> { candidat1, candidat2 });
            electionResult.CandidatsInSecondRound().Count.Should().Be(2);
        }
        [Then(@"no winner can be determined")]
        public void ThenNoWinnerCanBeDetermined()
        {
            winner.Should().Be(0);
        }
    }


}
